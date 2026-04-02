using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;
using OrderAPI.Controllers;
using OrderAPI.Models;
using OrderAPI.Services;

namespace OrderAPI.Tests.NUnit;

/// <summary>
/// NUnit tests for OrderController using Moq.
/// Mirrors the xUnit suite — same scenarios, NUnit syntax.
/// </summary>
[TestFixture]
public class OrderControllerTests
{
    // ─── Shared Setup ────────────────────────────────────────────────────────

    private Mock<IOrderService> _mockService = null!;
    private OrderController     _controller  = null!;

    [SetUp]
    public void SetUp()
    {
        _mockService = new Mock<IOrderService>();
        _controller  = new OrderController(_mockService.Object);
    }

    // ─── Test 1: Valid Order → 201 Created ───────────────────────────────────

    [Test]
    public async Task PlaceOrder_ValidOrder_ReturnsCreated()
    {
        // Arrange
        var order = new Order
        {
            CustomerId  = 1,
            ProductName = "Laptop",
            Quantity    = 2,
            TotalAmount = 1999.98m
        };

        // Service returns true → order placed successfully
        _mockService
            .Setup(s => s.PlaceOrderAsync(order))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.PlaceOrder(order);

        // Assert — must be 201 CreatedResult
        Assert.That(result, Is.InstanceOf<CreatedResult>());
        var createdResult = (CreatedResult)result;
        Assert.That(createdResult.StatusCode, Is.EqualTo(201));

        // Assert — returned value is the same order
        var returnedOrder = (Order)createdResult.Value!;
        Assert.Multiple(() =>
        {
            Assert.That(returnedOrder.ProductName, Is.EqualTo(order.ProductName));
            Assert.That(returnedOrder.Quantity,    Is.EqualTo(order.Quantity));
        });

        // Assert — service was called exactly once
        _mockService.Verify(s => s.PlaceOrderAsync(order), Times.Once);
    }

    // ─── Test 2: Invalid Order → 400 Bad Request ─────────────────────────────

    [Test]
    public async Task PlaceOrder_InvalidOrder_ReturnsBadRequest()
    {
        // Arrange
        var order = new Order
        {
            CustomerId  = 2,
            ProductName = "",       // invalid — empty product name
            Quantity    = 0,        // invalid — zero quantity
            TotalAmount = -10m      // invalid — negative amount
        };

        // Service returns false → order failed
        _mockService
            .Setup(s => s.PlaceOrderAsync(order))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.PlaceOrder(order);

        // Assert — must be 400 BadRequestResult
        Assert.That(result, Is.InstanceOf<BadRequestResult>());

        // Assert — service was called exactly once
        _mockService.Verify(s => s.PlaceOrderAsync(order), Times.Once);
    }

    // ─── Bonus Test 3: Parameterised Status Code Verification ─────────────────

    [TestCase(true,  201, Description = "Valid order → 201 Created")]
    [TestCase(false, 400, Description = "Invalid order → 400 Bad Request")]
    public async Task PlaceOrder_ReturnsExpectedStatusCode(bool serviceResult, int expectedStatusCode)
    {
        // Arrange
        var order = new Order
        {
            CustomerId  = 1,
            ProductName = "Mouse",
            Quantity    = 1,
            TotalAmount = 29.99m
        };

        _mockService
            .Setup(s => s.PlaceOrderAsync(order))
            .ReturnsAsync(serviceResult);

        // Act
        var result = await _controller.PlaceOrder(order);

        // Assert
        var statusCodeResult = result as IStatusCodeActionResult;
        Assert.That(statusCodeResult,             Is.Not.Null);
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(expectedStatusCode));
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using OrderAPI.Controllers;
using OrderAPI.Models;
using OrderAPI.Services;
using Xunit;

namespace OrderAPI.Tests.xUnit;

/// <summary>
/// xUnit tests for OrderController using Moq.
/// Tests both the success (201 Created) and failure (400 Bad Request) scenarios.
/// </summary>
public class OrderControllerTests
{
    // ─── Shared Setup ────────────────────────────────────────────────────────

    private readonly Mock<IOrderService> _mockService;
    private readonly OrderController     _controller;

    public OrderControllerTests()
    {
        _mockService = new Mock<IOrderService>();
        _controller  = new OrderController(_mockService.Object);
    }

    // ─── Test 1: Valid Order → 201 Created ───────────────────────────────────

    [Fact]
    public async Task PlaceOrder_ValidOrder_ReturnsCreated()
    {
        // Arrange
        var order = new Order
        {
            CustomerId   = 1,
            ProductName  = "Laptop",
            Quantity     = 2,
            TotalAmount  = 1999.98m
        };

        // Service returns true → order placed successfully
        _mockService
            .Setup(s => s.PlaceOrderAsync(order))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.PlaceOrder(order);

        // Assert — must be 201 CreatedResult
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, createdResult.StatusCode);

        // Assert — returned value is the same order
        var returnedOrder = Assert.IsType<Order>(createdResult.Value);
        Assert.Equal(order.ProductName, returnedOrder.ProductName);
        Assert.Equal(order.Quantity,    returnedOrder.Quantity);

        // Assert — service was called exactly once
        _mockService.Verify(s => s.PlaceOrderAsync(order), Times.Once);
    }

    // ─── Test 2: Invalid Order → 400 Bad Request ─────────────────────────────

    [Fact]
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
        Assert.IsType<BadRequestResult>(result);

        // Assert — service was called exactly once
        _mockService.Verify(s => s.PlaceOrderAsync(order), Times.Once);
    }

    // ─── Bonus Test 3: Parameterised Status Code Verification ─────────────────

    [Theory]
    [InlineData(true,  201)]
    [InlineData(false, 400)]
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
        Assert.NotNull(statusCodeResult);
        Assert.Equal(expectedStatusCode, statusCodeResult.StatusCode);
    }
}

using InventoryAPI.Controllers;
using InventoryAPI.Models;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;

namespace InventoryAPI.Tests.NUnit;

[TestFixture]
public class ProductControllerTests
{
    private Mock<IProductService> _mockService = null!;
    private ProductController     _controller  = null!;

    [SetUp]
    public void SetUp()
    {
        _mockService = new Mock<IProductService>();
        _controller  = new ProductController(_mockService.Object);
    }

    [Test]
    public async Task GetProduct_ExistingId_ReturnsOkWithProduct()
    {
        // Arrange
        var expectedProduct = new Product
        {
            Id            = 1,
            Name          = "Laptop",
            Description   = "High-performance laptop",
            Price         = 999.99m,
            StockQuantity = 10
        };

        _mockService
            .Setup(s => s.GetProductByIdAsync(1))
            .ReturnsAsync(expectedProduct);

        // Act
        var result = await _controller.GetProduct(1);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult      = (OkObjectResult)result;
        var actualProduct = (Product)okResult.Value!;

        Assert.Multiple(() =>
        {
            Assert.That(actualProduct.Id,            Is.EqualTo(expectedProduct.Id));
            Assert.That(actualProduct.Name,          Is.EqualTo(expectedProduct.Name));
            Assert.That(actualProduct.Price,         Is.EqualTo(expectedProduct.Price));
            Assert.That(actualProduct.StockQuantity, Is.EqualTo(expectedProduct.StockQuantity));
        });

        _mockService.Verify(s => s.GetProductByIdAsync(1), Times.Once);
    }

    [Test]
    public async Task GetProduct_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService
            .Setup(s => s.GetProductByIdAsync(99))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _controller.GetProduct(99);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
        _mockService.Verify(s => s.GetProductByIdAsync(99), Times.Once);
    }

    [TestCase(1,  200)]
    [TestCase(99, 404)]
    public async Task GetProduct_ReturnsExpectedStatusCode(int id, int expectedStatusCode)
    {
        // Arrange
        if (id == 1)
            _mockService.Setup(s => s.GetProductByIdAsync(id))
                        .ReturnsAsync(new Product { Id = id, Name = "Laptop" });
        else
            _mockService.Setup(s => s.GetProductByIdAsync(id))
                        .ReturnsAsync((Product?)null);

        // Act
        var result = await _controller.GetProduct(id);

        // Assert
        var statusCodeResult = result as IStatusCodeActionResult;
        Assert.That(statusCodeResult,             Is.Not.Null);
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(expectedStatusCode));
    }
}
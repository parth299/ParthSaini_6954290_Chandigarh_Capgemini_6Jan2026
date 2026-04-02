using InventoryAPI.Controllers;
using InventoryAPI.Models;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Xunit;

namespace InventoryAPI.Tests.xUnit;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController     _controller;

    public ProductControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller  = new ProductController(_mockService.Object);
    }

    [Fact]
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
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(expectedProduct.Id,            actualProduct.Id);
        Assert.Equal(expectedProduct.Name,          actualProduct.Name);
        Assert.Equal(expectedProduct.Price,         actualProduct.Price);
        Assert.Equal(expectedProduct.StockQuantity, actualProduct.StockQuantity);
        _mockService.Verify(s => s.GetProductByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetProduct_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService
            .Setup(s => s.GetProductByIdAsync(99))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _controller.GetProduct(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockService.Verify(s => s.GetProductByIdAsync(99), Times.Once);
    }

    [Theory]
    [InlineData(1,  200)]
    [InlineData(99, 404)]
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
        Assert.NotNull(statusCodeResult);
        Assert.Equal(expectedStatusCode, statusCodeResult.StatusCode);
    }
}
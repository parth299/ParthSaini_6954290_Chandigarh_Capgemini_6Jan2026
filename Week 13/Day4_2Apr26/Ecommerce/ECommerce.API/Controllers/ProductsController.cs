using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    // [Authorize(Roles = "Admin")]
[HttpPost("add-product")]
public async Task<IActionResult> AddProduct(Product product)
{
    await _productService.AddProduct(product);

    return Ok(product);
}

    // [Authorize(Roles = "User,Admin")]
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProducts();
        return Ok(products);
    }
}
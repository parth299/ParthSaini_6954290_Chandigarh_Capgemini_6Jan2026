using InventoryAPI.Models;

namespace InventoryAPI.Services;

/// <summary>
/// In-memory implementation of IProductService for demonstration purposes.
/// In a real application, this would interact with a database.
/// </summary>
public class ProductService : IProductService
{
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop",       Description = "High-performance laptop", Price = 999.99m,  StockQuantity = 10 },
        new Product { Id = 2, Name = "Mouse",        Description = "Wireless mouse",          Price = 29.99m,   StockQuantity = 50 },
        new Product { Id = 3, Name = "Keyboard",     Description = "Mechanical keyboard",     Price = 79.99m,   StockQuantity = 30 },
        new Product { Id = 4, Name = "Monitor",      Description = "4K UHD monitor",          Price = 449.99m,  StockQuantity = 15 },
        new Product { Id = 5, Name = "Headphones",   Description = "Noise-cancelling",        Price = 199.99m,  StockQuantity = 25 },
    };

    public Task<Product?> GetProductByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }
}

using Microsoft.Extensions.Caching.Memory;

public class ProductService
{
    private readonly IMemoryCache _cache;

    public ProductService(IMemoryCache cache)
    {
        _cache = cache;
    }

    // Get Products
    public async Task<List<Product>> GetProducts()
    {
        if (_cache.TryGetValue("products", out List<Product>? cached))
            return cached!;

        // Fake DB data (matches your Product model)
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop" },
            new Product { Id = 2, Name = "Mobile" }
        };

        _cache.Set("products", products, TimeSpan.FromMinutes(5));

        return await Task.FromResult(products);
    }

    // Add Product
    public async Task AddProduct(Product product)
    {
        List<Product> products;

        if (_cache.TryGetValue("products", out List<Product>? cached))
        {
            products = cached!;
        }
        else
        {
            products = new List<Product>();
        }

        product.Id = products.Count + 1;

        products.Add(product);

        _cache.Set("products", products, TimeSpan.FromMinutes(5));

        await Task.CompletedTask;
    }
}
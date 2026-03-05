class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

class Category : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<IProduct> Products { get; set; }

    public Category(int id, string name)
    {
        Id = id;
        Name = name;
        Products = new List<IProduct>();
    }

    public void AddProduct(IProduct product)
    {
        Products.Add(product);
    }
}

class Company : ICompany
{
    private string companyName;
    private List<ICategory> categories = new List<ICategory>();

    public Company(string name)
    {
        companyName = name;
    }

    public void AddCategory(ICategory category)
    {
        categories.Add(category);
    }

    public string GetTopCategoryNameByProductCount()
    {
        var top = categories
            .OrderByDescending(c => c.Products.Count)
            .FirstOrDefault();

        return top?.Name;
    }

    public List<IProduct> GetProductsBelongsToMultipleCategory()
    {
        return categories
            .SelectMany(c => c.Products)
            .GroupBy(p => p.Id)
            .Where(g => g.Count() > 1)
            .Select(g => g.First())
            .ToList();
    }

    public (string categoryName, decimal totalValue) GetTopCategoryBySumOfProductPrices()
    {
        var result = categories
            .Select(c => new
            {
                Name = c.Name,
                Total = c.Products.Sum(p => p.Price)
            })
            .OrderByDescending(x => x.Total)
            .FirstOrDefault();

        return (result.Name, result.Total);
    }

    public List<(ICategory category, decimal totalValue)> GetCategoriesWithSumOfTheProductPrices()
    {
        return categories
            .Select(c => (c, c.Products.Sum(p => p.Price)))
            .ToList();
    }
}
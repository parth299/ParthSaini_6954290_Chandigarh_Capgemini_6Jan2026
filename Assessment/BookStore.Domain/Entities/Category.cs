namespace BookStore.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;

    // Navigation properties
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

namespace BookStore.Domain.Entities;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }

    // Navigation properties
    public Category? Category { get; set; }
    public Author? Author { get; set; }
    public Publisher? Publisher { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}

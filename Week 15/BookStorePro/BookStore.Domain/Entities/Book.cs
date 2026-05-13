namespace BookStore.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }

    public string ISBN { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    // Navigation
    public Category Category { get; set; }

    public Author Author { get; set; }

    public Publisher Publisher { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<Wishlist> Wishlists { get; set; }
}
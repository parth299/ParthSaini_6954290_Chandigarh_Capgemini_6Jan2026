namespace BookStore.Domain.Entities;

public class Wishlist
{
    public int UserId { get; set; }

    public int BookId { get; set; }

    // Navigation
    public User User { get; set; }

    public Book Book { get; set; }
}
namespace BookStore.Domain.Entities;

public class Review
{
    public int ReviewId { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int Rating { get; set; } // 1-5
    public string? Comment { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public Book? Book { get; set; }
}

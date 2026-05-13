namespace BookStore.Domain.Entities;

public class Review : BaseEntity
{
    public int UserId { get; set; }

    public int BookId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }

    // Navigation
    public User User { get; set; }

    public Book Book { get; set; }
}
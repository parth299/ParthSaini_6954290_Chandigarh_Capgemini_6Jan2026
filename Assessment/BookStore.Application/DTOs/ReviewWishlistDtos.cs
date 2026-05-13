namespace BookStore.Application.DTOs;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}

public class ReviewCreateDto
{
    public int BookId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}

public class WishlistDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = null!;
}

public class WishlistCreateDto
{
    public int BookId { get; set; }
}

namespace BookStore.Application.DTOs;

public class BookDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    public int PublisherId { get; set; }
    public string PublisherName { get; set; } = null!;
}

public class BookCreateDto
{
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }
}

public class BookUpdateDto
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
}

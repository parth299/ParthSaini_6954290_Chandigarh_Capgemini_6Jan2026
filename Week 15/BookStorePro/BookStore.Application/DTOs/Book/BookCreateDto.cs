namespace BookStore.Application.DTOs.Book;

public class BookCreateDto
{
    public string Title { get; set; }

    public string ISBN { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }
}
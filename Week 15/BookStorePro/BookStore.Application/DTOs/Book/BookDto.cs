namespace BookStore.Application.DTOs.Book;

public class BookDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string ISBN { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string ImageUrl { get; set; }

    public string CategoryName { get; set; }

    public string AuthorName { get; set; }

    public string PublisherName { get; set; }
}
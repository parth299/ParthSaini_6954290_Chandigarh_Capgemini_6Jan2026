namespace BookStore.Web.Models.DTOs
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Pages { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? ImageUrl { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
    }

    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }
    }

    public class PublisherDto
    {
        public int PublisherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
    }

    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

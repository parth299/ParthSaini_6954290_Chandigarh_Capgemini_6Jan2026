namespace BookStore.Application.DTOs;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
}

public class CategoryCreateDto
{
    public string Name { get; set; } = null!;
}

public class AuthorDto
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = null!;
}

public class AuthorCreateDto
{
    public string Name { get; set; } = null!;
}

public class PublisherDto
{
    public int PublisherId { get; set; }
    public string Name { get; set; } = null!;
}

public class PublisherCreateDto
{
    public string Name { get; set; } = null!;
}

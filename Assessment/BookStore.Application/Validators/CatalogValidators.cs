using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validators;

public class BookCreateValidator : AbstractValidator<BookCreateDto>
{
    public BookCreateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(3, 200).WithMessage("Title must be between 3 and 200 characters");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required")
            .Length(10, 20).WithMessage("ISBN must be between 10 and 20 characters");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Stock)
            .NotEmpty().WithMessage("Stock is required")
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category is required")
            .GreaterThan(0).WithMessage("Valid category is required");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author is required")
            .GreaterThan(0).WithMessage("Valid author is required");

        RuleFor(x => x.PublisherId)
            .NotEmpty().WithMessage("Publisher is required")
            .GreaterThan(0).WithMessage("Valid publisher is required");
    }
}

public class BookUpdateValidator : AbstractValidator<BookUpdateDto>
{
    public BookUpdateValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("Book ID is required")
            .GreaterThan(0).WithMessage("Valid book ID is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(3, 200).WithMessage("Title must be between 3 and 200 characters");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required")
            .Length(10, 20).WithMessage("ISBN must be between 10 and 20 characters");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Stock)
            .NotEmpty().WithMessage("Stock is required")
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category is required")
            .GreaterThan(0).WithMessage("Valid category is required");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author is required")
            .GreaterThan(0).WithMessage("Valid author is required");

        RuleFor(x => x.PublisherId)
            .NotEmpty().WithMessage("Publisher is required")
            .GreaterThan(0).WithMessage("Valid publisher is required");
    }
}

public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .Length(2, 100).WithMessage("Category name must be between 2 and 100 characters");
    }
}

public class AuthorCreateValidator : AbstractValidator<AuthorCreateDto>
{
    public AuthorCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Author name is required")
            .Length(3, 100).WithMessage("Author name must be between 3 and 100 characters");
    }
}

public class PublisherCreateValidator : AbstractValidator<PublisherCreateDto>
{
    public PublisherCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Publisher name is required")
            .Length(2, 100).WithMessage("Publisher name must be between 2 and 100 characters");
    }
}

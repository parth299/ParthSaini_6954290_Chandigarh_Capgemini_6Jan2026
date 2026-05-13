using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validators;

public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required")
            .GreaterThan(0).WithMessage("Valid user ID is required");

        RuleFor(x => x.OrderItems)
            .NotEmpty().WithMessage("Order must contain at least one item")
            .Must(items => items.Count > 0).WithMessage("Order must contain at least one item");

        RuleForEach(x => x.OrderItems)
            .SetValidator(new OrderItemCreateValidator());
    }
}

public class OrderItemCreateValidator : AbstractValidator<OrderItemCreateDto>
{
    public OrderItemCreateValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("Book ID is required")
            .GreaterThan(0).WithMessage("Valid book ID is required");

        RuleFor(x => x.Qty)
            .NotEmpty().WithMessage("Quantity is required")
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}

public class OrderStatusUpdateValidator : AbstractValidator<OrderStatusUpdateDto>
{
    private static readonly List<string> ValidStatuses = new() { "Pending", "Confirmed", "Shipped", "Delivered", "Cancelled" };

    public OrderStatusUpdateValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required")
            .GreaterThan(0).WithMessage("Valid order ID is required");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .Must(status => ValidStatuses.Contains(status))
            .WithMessage($"Status must be one of: {string.Join(", ", ValidStatuses)}");
    }
}

public class ReviewCreateValidator : AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("Book ID is required")
            .GreaterThan(0).WithMessage("Valid book ID is required");

        RuleFor(x => x.Rating)
            .NotEmpty().WithMessage("Rating is required")
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");

        RuleFor(x => x.Comment)
            .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters");
    }
}

public class WishlistCreateValidator : AbstractValidator<WishlistCreateDto>
{
    public WishlistCreateValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("Book ID is required")
            .GreaterThan(0).WithMessage("Valid book ID is required");
    }
}

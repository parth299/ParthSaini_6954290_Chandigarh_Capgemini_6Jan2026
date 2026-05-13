using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validators;

public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}

public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .Length(3, 100).WithMessage("Full name must be between 3 and 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Length(10, 15).WithMessage("Phone must be between 10 and 15 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(8, 50).WithMessage("Password must be between 8 and 50 characters")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
            .Matches(@"[!@#$%^&*]").WithMessage("Password must contain at least one special character (!@#$%^&*)");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}

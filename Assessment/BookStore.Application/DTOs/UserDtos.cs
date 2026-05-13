namespace BookStore.Application.DTOs;

public class UserLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class UserRegisterDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string Phone { get; set; } = null!;
}

public class UserResponseDto
{
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}

public class UserProfileDto
{
    public int ProfileId { get; set; }
    public int UserId { get; set; }
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Pincode { get; set; } = null!;
}

public class AuthResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public string? Token { get; set; }
    public UserResponseDto? User { get; set; }
}

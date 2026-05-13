using BookStore.Application.Interfaces;

namespace BookStore.Application.Services;

public class TokenService
    : ITokenService
{
    public string GenerateToken(
        string email,
        string role)
    {
        // JWT logic later

        return "TOKEN_PLACEHOLDER";
    }
}
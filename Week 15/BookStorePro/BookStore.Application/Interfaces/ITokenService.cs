namespace BookStore.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(
        string email,
        string role);
}
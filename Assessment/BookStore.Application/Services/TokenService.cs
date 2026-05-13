using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Application.Services;

public class TokenService : ITokenService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expirationMinutes;

    public TokenService(string secretKey, string issuer = "BookStoreAPI", string audience = "BookStoreClients", int expirationMinutes = 60)
    {
        _secretKey = secretKey;
        _issuer = issuer;
        _audience = audience;
        _expirationMinutes = expirationMinutes;
    }

    public string GenerateToken(int userId, string email, int roleId, string roleName)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, roleName),
            new("roleId", roleId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
            signingCredentials: creds
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}

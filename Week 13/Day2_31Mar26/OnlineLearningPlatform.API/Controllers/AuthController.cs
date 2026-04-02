using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.API.Data;
using OnlineLearningPlatform.API.DTOs;
using OnlineLearningPlatform.API.Helpers;
using OnlineLearningPlatform.API.Models;

namespace OnlineLearningPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(
        AppDbContext context,
        JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    // REGISTER

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterDto dto)
    {
        if (await _context.Users
            .AnyAsync(u => u.Username == dto.Username))
        {
            return BadRequest(
                new { error = "Username already exists" });
        }

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash =
                PasswordHelper.HashPassword(
                    dto.Password),
            Role = dto.Role
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok(
            new { message = "User registered" });
    }

    // LOGIN

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginDto dto)
    {
        var hash =
            PasswordHelper.HashPassword(
                dto.Password);

        var user =
            await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Username == dto.Username &&
                    u.PasswordHash == hash);

        if (user == null)
        {
            return Unauthorized(
                new { error = "Invalid credentials" });
        }

        var token =
            _jwtService.GenerateToken(user);

        return Ok(new
        {
            token
        });
    }
}
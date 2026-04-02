using Microsoft.AspNetCore.Mvc;
using TransactionApi.Data;
using TransactionApi.DTOs;
using TransactionApi.Services;
using Microsoft.EntityFrameworkCore;
using TransactionApi.Helpers;

namespace TransactionApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(
            ApplicationDbContext context,
            TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult>
            Register(RegisterDto registerDto)
        {
            // Check duplicate username

            var exists =
                await _context.Users
                    .AnyAsync(u =>
                        u.Username ==
                        registerDto.Username);

            if (exists)
                return BadRequest(
                    "Username already exists");

            // Create user

            var user = new Models.User
            {
                Username = registerDto.Username,

                PasswordHash =
                    PasswordHelper.HashPassword(
                        registerDto.Password)
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            // Generate token

            var token =
                _tokenService.CreateToken(user);

            return Ok(new
            {
                token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginDto loginDto)
        {
            var user =
                await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Username == loginDto.Username);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var isValid =
                PasswordHelper.VerifyPassword(
                    loginDto.Password,
                    user.PasswordHash);

            if (!isValid)
                return Unauthorized("Invalid credentials");

            var token =
                _tokenService.CreateToken(user);

            return Ok(new { token });
        }
    }
}
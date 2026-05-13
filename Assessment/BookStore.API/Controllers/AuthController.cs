using BookStore.API.Middleware;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(UserLoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (!result.Success)
            return Unauthorized(result);

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(UserRegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        if (!result.Success)
            return BadRequest(result);

        return CreatedAtAction(nameof(Register), result);
    }
}

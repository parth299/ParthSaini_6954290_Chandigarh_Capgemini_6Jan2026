using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WishlistDto>>> GetMyWishlist()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var wishlists = await _wishlistService.GetUserWishlistAsync(userId);
        return Ok(wishlists);
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(WishlistCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        await _wishlistService.AddToWishlistAsync(userId, dto);
        return CreatedAtAction(nameof(GetMyWishlist), null);
    }

    [HttpDelete("{bookId}")]
    public async Task<IActionResult> RemoveFromWishlist(int bookId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        await _wishlistService.RemoveFromWishlistAsync(userId, bookId);
        return NoContent();
    }
}

using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("book/{bookId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetBookReviews(int bookId)
    {
        var reviews = await _reviewService.GetBookReviewsAsync(bookId);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> CreateReview(ReviewCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var review = await _reviewService.CreateReviewAsync(userId, dto);
        return CreatedAtAction(nameof(GetBookReviews), new { bookId = review.BookId }, review);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        await _reviewService.DeleteReviewAsync(id);
        return NoContent();
    }
}

using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class PublishersController : ControllerBase
{
    private readonly IPublisherService _publisherService;

    public PublishersController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<PublisherDto>>> GetAllPublishers()
    {
        var publishers = await _publisherService.GetAllPublishersAsync();
        return Ok(publishers);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
    {
        var publisher = await _publisherService.GetPublisherByIdAsync(id);
        if (publisher == null)
            return NotFound(new { message = "Publisher not found" });

        return Ok(publisher);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PublisherDto>> CreatePublisher(PublisherCreateDto dto)
    {
        var publisher = await _publisherService.CreatePublisherAsync(dto);
        return CreatedAtAction(nameof(GetPublisher), new { id = publisher.PublisherId }, publisher);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePublisher(int id, PublisherCreateDto dto)
    {
        await _publisherService.UpdatePublisherAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        await _publisherService.DeletePublisherAsync(id);
        return NoContent();
    }
}

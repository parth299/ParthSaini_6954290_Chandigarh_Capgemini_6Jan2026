using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        if (author == null)
            return NotFound(new { message = "Author not found" });

        return Ok(author);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorCreateDto dto)
    {
        var author = await _authorService.CreateAuthorAsync(dto);
        return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, author);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorCreateDto dto)
    {
        await _authorService.UpdateAuthorAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return NoContent();
    }
}

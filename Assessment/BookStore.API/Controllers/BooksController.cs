using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
            return NotFound(new { message = "Book not found" });

        return Ok(book);
    }

    [HttpGet("category/{categoryId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByCategory(int categoryId)
    {
        var books = await _bookService.GetBooksByCategoryAsync(categoryId);
        return Ok(books);
    }

    [HttpGet("search/{keyword}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks(string keyword)
    {
        var books = await _bookService.SearchBooksAsync(keyword);
        return Ok(books);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BookDto>> CreateBook(BookCreateDto dto)
    {
        var book = await _bookService.CreateBookAsync(dto);
        return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook(int id, BookUpdateDto dto)
    {
        if (id != dto.BookId)
            return BadRequest(new { message = "ID mismatch" });

        await _bookService.UpdateBookAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteBookAsync(id);
        return NoContent();
    }
}

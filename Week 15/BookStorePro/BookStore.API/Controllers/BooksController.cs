using Microsoft.AspNetCore.Mvc;
using BookStore.Application.Interfaces;
using BookStore.Application.DTOs.Book;

namespace BookStore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(
        IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult>
        GetAll()
    {
        var books =
            await _bookService
            .GetAllBooksAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult>
        GetById(int id)
    {
        var book =
            await _bookService
            .GetBookByIdAsync(id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult>
        Create(
            BookCreateDto dto)
    {
        await _bookService
            .CreateBookAsync(dto);

        return Ok(
            "Book created successfully");
    }

    [HttpPut]
    public async Task<IActionResult>
        Update(
            BookUpdateDto dto)
    {
        await _bookService
            .UpdateBookAsync(dto);

        return Ok(
            "Book updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>
        Delete(int id)
    {
        await _bookService
            .DeleteBookAsync(id);

        return Ok(
            "Book deleted successfully");
    }
}
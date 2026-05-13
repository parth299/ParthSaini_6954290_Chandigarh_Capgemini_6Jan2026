using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.API.Data;
using BookStore.API.Models;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (id != book.Id)
                return BadRequest();

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
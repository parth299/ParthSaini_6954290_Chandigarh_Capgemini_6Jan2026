using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using BookStore.Application.Interfaces;

namespace BookStore.Infrastructure.Repositories;

public class BookRepository
    : GenericRepository<Book>,
      IBookRepository
{
    public BookRepository(
        ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Book>>
        GetBooksWithCategoryAsync()
    {
        return await _context.Books
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Include(x => x.Publisher)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>>
        GetBooksInStockAsync()
    {
        return await _context.Books
            .Where(x => x.Stock > 0)
            .ToListAsync();
    }
}
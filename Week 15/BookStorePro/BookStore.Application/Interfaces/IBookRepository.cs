using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<IEnumerable<Book>> GetBooksWithCategoryAsync();

    Task<IEnumerable<Book>> GetBooksInStockAsync();
}

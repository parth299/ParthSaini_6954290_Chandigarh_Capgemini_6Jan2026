using BookStore.Application.DTOs.Book;

namespace BookStore.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>>
        GetAllBooksAsync();

    Task<BookDto>
        GetBookByIdAsync(int id);

    Task CreateBookAsync(
        BookCreateDto dto);

    Task UpdateBookAsync(
        BookUpdateDto dto);

    Task DeleteBookAsync(int id);
}
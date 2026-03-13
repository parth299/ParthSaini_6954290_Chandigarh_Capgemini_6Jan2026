// Repositories/IBookRepository.cs
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IBookRepository
    {
        Book GetBookById(int id);

        List<Book> GetAllBooks();
    }
}
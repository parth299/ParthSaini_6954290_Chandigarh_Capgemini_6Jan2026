// Repositories/BookRepository.cs
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books;

        public BookRepository()
        {
            _books = new List<Book>()
            {
                new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 },
                new Book { Id = 2, Title = "Design Patterns", Author = "Erich Gamma", Year = 1994 },
                new Book { Id = 3, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Year = 1999 }
            };
        }

        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }
    }
}
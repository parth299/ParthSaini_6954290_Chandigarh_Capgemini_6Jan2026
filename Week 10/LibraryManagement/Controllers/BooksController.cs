// Controllers/BooksController.cs
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Repositories;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Details(int id)
        {
            var book = _repository.GetBookById(id);

            if (book == null)
                return NotFound();

            return View(book);
        }
    }
}
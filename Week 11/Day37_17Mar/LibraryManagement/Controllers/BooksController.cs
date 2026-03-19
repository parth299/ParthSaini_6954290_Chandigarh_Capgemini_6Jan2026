using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
	public class BooksController : Controller
	{
		// Temporary in-memory list (instead of DB)
		private static List<Book> books = new List<Book>()
		{
			new Book { Id = 1, Title = "C# Basics", Author = "John", PublishedYear = 2020, Genre = "Programming" },
			new Book { Id = 2, Title = "ASP.NET Core", Author = "Smith", PublishedYear = 2022, Genre = "Web Development" }
		};

		// GET: Books
		public IActionResult Index()
		{
			var bookVMList = books.Select(b => new BookViewModel
			{
				Book = b,
				IsAvailable = true,
				BorrowerName = null
			}).ToList();

			ViewBag.Message = "Welcome to Library System 📚";
			ViewData["TotalBooks"] = books.Count;

			return View(bookVMList);
		}

		// GET: Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Create
		[HttpPost]
		public IActionResult Create(Book book)
		{
			if (ModelState.IsValid)
			{
				book.Id = books.Count + 1;
				books.Add(book);

				return RedirectToAction("Index");
			}

			return View(book);
		}
	}
}
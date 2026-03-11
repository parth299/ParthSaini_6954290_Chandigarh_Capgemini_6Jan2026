using Microsoft.AspNetCore.Mvc;

public class BooksController : Controller {

    private readonly BookDbContext _context;

    public BooksController(BookDbContext context) {
        _context = context;
    }

    public IActionResult Index() {
        var books = _context.books.ToList();
        return View(books);
    }

    public IActionResult AddBook() {
        return View();
    }

    public IActionResult EditBook(int id) {
        var book = _context.books.FirstOrDefault(b => b.BookId == id);
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookModel book)
    {
        if (ModelState.IsValid)
        {
            _context.books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(book);
    }

}
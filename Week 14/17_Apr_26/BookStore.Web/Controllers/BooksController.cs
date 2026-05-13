using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using BookStore.Web.Models;

public class BooksController : Controller
{
    private readonly IHttpClientFactory _factory;

    public BooksController(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    // ======================
    // GET ALL BOOKS
    // ======================

    public async Task<IActionResult> Index()
    {
        var books = new List<Book>();

        var client = _factory.CreateClient("BookAPI");

        var response =
            await client.GetAsync("api/books");

        if (response.IsSuccessStatusCode)
        {
            var json =
                await response.Content.ReadAsStringAsync();

            books =
                JsonConvert.DeserializeObject<List<Book>>(json)
                ?? new List<Book>();
        }

        return View(books);
    }

    // ======================
    // CREATE BOOK
    // ======================

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        var client = _factory.CreateClient("BookAPI");

        var json =
            JsonConvert.SerializeObject(book);

        var content =
            new StringContent(json,
                              Encoding.UTF8,
                              "application/json");

        await client.PostAsync("api/books", content);

        return RedirectToAction("Index");
    }

    // ======================
    // EDIT BOOK
    // ======================

    public async Task<IActionResult> Edit(int id)
    {
        var client = _factory.CreateClient("BookAPI");

        var response =
            await client.GetAsync($"api/books/{id}");

        var json =
            await response.Content.ReadAsStringAsync();

        var book =
            JsonConvert.DeserializeObject<Book>(json);

        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Book book)
    {
        var client = _factory.CreateClient("BookAPI");

        var json =
            JsonConvert.SerializeObject(book);

        var content =
            new StringContent(json,
                              Encoding.UTF8,
                              "application/json");

        await client.PutAsync(
            $"api/books/{book.Id}",
            content);

        return RedirectToAction("Index");
    }

    // ======================
    // DELETE BOOK
    // ======================

    public async Task<IActionResult> Delete(int id)
    {
        var client = _factory.CreateClient("BookAPI");

        await client.DeleteAsync(
            $"api/books/{id}");

        return RedirectToAction("Index");
    }
}
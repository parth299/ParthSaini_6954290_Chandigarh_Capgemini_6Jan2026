using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookStore.Web.Models;
using BookStore.Web.Services;
using BookStore.Web.ViewModels;
using BookStore.Web.Models.DTOs;

namespace BookStore.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApiService _apiService;

    public HomeController(ILogger<HomeController> logger, ApiService apiService)
    {
        _logger = logger;
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel();

        try
        {
            // Get featured books (first 8 books)
            var allBooks = await _apiService.GetBooksAsync();
            viewModel.FeaturedBooks = allBooks?.Take(8).ToList() ?? new List<BookDto>();

            // Get all categories
            viewModel.Categories = await _apiService.GetCategoriesAsync() ?? new List<CategoryDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading home page data");
            // Continue with empty data
        }

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

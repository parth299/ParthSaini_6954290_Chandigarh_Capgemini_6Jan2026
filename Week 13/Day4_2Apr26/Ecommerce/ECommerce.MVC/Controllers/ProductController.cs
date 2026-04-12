using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using ECommerce.MVC.Models;

namespace ECommerce.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        // Load products
        public async Task<IActionResult> Index()
        {
            // Get token from session
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response =
                await _httpClient.GetAsync(
                    "http://localhost:5293/api/products"
                );

            if (response.IsSuccessStatusCode)
            {
                var json =
                    await response.Content.ReadAsStringAsync();

                var products =
                    JsonSerializer.Deserialize<List<Product>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                return View(products);
            }

            return View(new List<Product>());
        }

        // Show Create form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Save Product
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            // var token = HttpContext.Session.GetString("JWToken");

            // if (!string.IsNullOrEmpty(token))
            // {
            //     _httpClient.DefaultRequestHeaders.Authorization =
            //         new AuthenticationHeaderValue("Bearer", token);
            // }

            var json = JsonSerializer.Serialize(product);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var response =
                await _httpClient.PostAsync(
                    "http://localhost:5293/api/products/add-product",
                    content
                );

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }
    }
}
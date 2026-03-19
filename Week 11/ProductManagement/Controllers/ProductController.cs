using Microsoft.AspNetCore.Mvc;
using ProductManagement.Filters;
using System;
using System.Collections.Generic;

namespace ProductManagement.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))] // Apply logging filter
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            // 🔥 Force exception to test filter
            throw new Exception("Test exception in Product Index");

            // Normal code (commented for testing)
            /*
            var products = new List<string>
            {
                "Laptop",
                "Mobile",
                "Tablet"
            };

            return View(products);
            */
        }
    }
}
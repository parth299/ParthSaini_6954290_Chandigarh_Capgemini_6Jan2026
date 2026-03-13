using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return Content("Admin Dashboard - Authenticated Only");
        }

        public IActionResult Settings()
        {
            return Content("Admin Settings Page");
        }
    }
}
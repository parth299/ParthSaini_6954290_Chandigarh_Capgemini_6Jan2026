using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // 🔒 Protected page
        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
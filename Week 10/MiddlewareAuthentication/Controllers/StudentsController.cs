using Microsoft.AspNetCore.Mvc;
using MyApp.Services;

namespace MyApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IAuthService _authService;

        public StudentsController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return Content("Public Students Page");
        }

        public IActionResult Login()
        {
            return Content("Login Page (POST username/password)");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (_authService.ValidateUser(username, password))
            {
                HttpContext.Session.SetString("User", username);
                return Redirect("/Admin/Dashboard");
            }

            return Content("Invalid Login");
        }
    }
}
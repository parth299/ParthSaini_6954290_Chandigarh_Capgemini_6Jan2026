using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SessionLoginManagement.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                // ✅ Store username in session
                HttpContext.Session.SetString("Username", username);

                return RedirectToAction("Dashboard");
            }

            // ❌ Invalid login
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        // GET: Dashboard
        public IActionResult Dashboard()
        {
            var username = HttpContext.Session.GetString("Username");

            // ✅ Protect route
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Username = username;
            return View();
        }

        // Logout
        public IActionResult Logout()
        {
            // ✅ Clear session
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineLearningPlatform.Web.Services;
using OnlineLearningPlatform.Web.ViewModels;

namespace OnlineLearningPlatform.Web.Controllers;

public class AuthController : Controller
{
    private readonly ApiService _api;

    public AuthController(ApiService api)
    {
        _api = api;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult>
    Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result =
            await _api.PostAsync(
                "Auth/register",
                model);

        Console.WriteLine(result);

        return RedirectToAction("Login");
    }

    // LOGIN PAGE

    public IActionResult Login()
    {
        return View();
    }

    // LOGIN POST

    [HttpPost]
    public async Task<IActionResult>
    Login(string username, string password)
    {
        var data = new
        {
            username,
            password
        };

        var result =
            await _api.PostAsync(
                "Auth/login",
                data);

        // DEBUG — show raw response
        Console.WriteLine("API RESPONSE:");
        Console.WriteLine(result);

        if (string.IsNullOrEmpty(result))
        {
            ViewBag.Error =
                "Empty response from API";

            return View();
        }

        try
        {
            var json =
                JObject.Parse(result);

            if (json["token"] != null)
            {
                HttpContext.Session.SetString(
                    "token",
                    json["token"].ToString());

                return RedirectToAction(
                    "Index",
                    "Course");
            }
        }
        catch
        {
            ViewBag.Error =
                "Invalid response from API";

            return View();
        }

        ViewBag.Error =
            "Invalid login";

        return View();
    }

    // LOGOUT

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction(
            "Login");
    }
}
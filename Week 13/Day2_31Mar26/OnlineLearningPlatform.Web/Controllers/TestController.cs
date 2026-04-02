using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Web.Services;

namespace OnlineLearningPlatform.Web.Controllers;

public class TestController : Controller
{
    private readonly ApiService _api;

    public TestController(ApiService api)
    {
        _api = api;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Student()
    {
        var token = HttpContext.Session.GetString("token");
        if (token == null)
            return RedirectToAction("Login", "Auth");

        var response = await _api.GetAsync("Test/student", token);
        ViewBag.StudentResponse = response;
        return View("Index");
    }

    public async Task<IActionResult> Instructor()
    {
        var token = HttpContext.Session.GetString("token");
        if (token == null)
            return RedirectToAction("Login", "Auth");

        var response = await _api.GetAsync("Test/instructor", token);
        ViewBag.InstructorResponse = response;
        return View("Index");
    }
}
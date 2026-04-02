using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineLearningPlatform.Web.Services;
using OnlineLearningPlatform.Web.ViewModels;

namespace OnlineLearningPlatform.Web.Controllers;

public class CourseController : Controller
{
    private readonly ApiService _api;

    public CourseController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult>
    Details(int id)
    {
        var token =
            HttpContext.Session
                .GetString("token");

        var courseResult =
            await _api.GetAsync(
                $"Course/{id}",
                token);

        var lessonResult =
            await _api.GetAsync(
                $"Lesson/course/{id}",
                token);

        var course =
            JsonConvert.DeserializeObject<
                CourseViewModel>(courseResult);

        var lessons =
            JsonConvert.DeserializeObject<
                List<LessonViewModel>>(lessonResult);

        ViewBag.Lessons = lessons;

        return View(course);
    }

    // LIST COURSES

    public async Task<IActionResult>
    Index()
    {
        var token =
            HttpContext.Session
                .GetString("token");

        if (token == null)
            return RedirectToAction(
                "Login",
                "Auth");

        var result =
            await _api.GetAsync(
                "Course",
                token);

        var courses =
            JsonConvert.DeserializeObject<
                List<CourseViewModel>>(result);

        return View(courses);
    }

    public async Task<IActionResult>
    Enroll(int id)
    {
        var token =
            HttpContext.Session
                .GetString("token");

        await _api.PostAsync(
            $"Enrollment/{id}",
            new { },
            token);

        return RedirectToAction(
            "Index");
    }

    public async Task<IActionResult>
    MyEnrollments()
    {
        var token =
            HttpContext.Session
                .GetString("token");

        if (token == null)
            return RedirectToAction(
                "Login",
                "Auth");

        var result = await _api.GetAsync(
            "Enrollment/me",
            token);

        var courses =
            JsonConvert.DeserializeObject<
                List<CourseViewModel>>(result);

        return View(courses);
    }

    // CREATE PAGE

    public IActionResult Create()
    {
        return View();
    }

    // CREATE POST

    [HttpPost]
    public async Task<IActionResult>
    Create(
        string title,
        string description)
    {
        Console.WriteLine("Reaching here");
        var token =
            HttpContext.Session
                .GetString("token");

        var data =
            new
            {
                title,
                description
            };

        await _api.PostAsync(
            "Course",
            data,
            token);

        return RedirectToAction(
            "Index");
    }

    // EDIT PAGE

    public async Task<IActionResult>
    Edit(int id)
    {
        var token =
            HttpContext.Session
                .GetString("token");

        if (token == null)
            return RedirectToAction(
                "Login",
                "Auth");

        var result =
            await _api.GetAsync(
                $"Course/{id}",
                token);

        var course =
            JsonConvert.DeserializeObject<CourseViewModel>(result);

        return View(course);
    }

    // EDIT POST

    [HttpPost]
    public async Task<IActionResult>
    Edit(CourseViewModel model)
    {
        var token =
            HttpContext.Session
                .GetString("token");

        if (token == null)
            return RedirectToAction(
                "Login",
                "Auth");

        var data = new
        {
            id = model.Id,
            title = model.Title,
            description = model.Description
        };

        await _api.PutAsync(
            $"Course/{model.Id}",
            data,
            token);

        return RedirectToAction("Index");
    }

    // DELETE

    [HttpPost]
    public async Task<IActionResult>
    Delete(int id)
    {
        var token =
            HttpContext.Session
                .GetString("token");

        if (token == null)
            return RedirectToAction(
                "Login",
                "Auth");

        await _api.DeleteAsync(
            $"Course/{id}",
            token);

        return RedirectToAction("Index");
    }

    // ADD LESSON (from course details)

    [HttpPost]
    public async Task<IActionResult>
    AddLesson(int courseId, string title, string content)
    {
        var token =
            HttpContext.Session
                .GetString("token");

        if (token == null)
            return RedirectToAction(
                "Login",
                "Auth");

        var data = new
        {
            courseId,
            title,
            content
        };

        await _api.PostAsync(
            "Lesson",
            data,
            token);

        return RedirectToAction(
            "Details",
            new { id = courseId });
    }
}
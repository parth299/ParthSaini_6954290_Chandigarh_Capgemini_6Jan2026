// Controllers/StudentsController.cs
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;
using StudentPortal.Services;

namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IRequestLogService _logService;

        public StudentsController(IRequestLogService logService)
        {
            _logService = logService;
        }

        public IActionResult Index()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Rahul", Course = "Computer Science"},
                new Student { Id = 2, Name = "Simran", Course = "Information Technology"},
                new Student { Id = 3, Name = "Aman", Course = "Software Engineering"}
            };

            ViewBag.RequestLogs = _logService.GetLogs();

            return View(students);
        }
    }
}
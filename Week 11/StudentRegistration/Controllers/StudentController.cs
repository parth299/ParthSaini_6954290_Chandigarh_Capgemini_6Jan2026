using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentRegistration.Controllers
{
    public class StudentController : Controller
    {
        // Simulating database
        private static List<Student> students = new List<Student>();
        private static int nextId = 1;

        // GET: Student/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Student/Register
        [HttpPost]
        public IActionResult Register(Student student)
        {
            // Model Binding automatically maps form data

            if (ModelState.IsValid)
            {
                student.Id = nextId++;
                students.Add(student);

                // Store success message
                TempData["SuccessMessage"] = "Student registered successfully!";

                return RedirectToAction("Details", new { id = student.Id });
            }

            return View(student);
        }

        // GET: Student/Details/{id}
        public IActionResult Details(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
    }
}
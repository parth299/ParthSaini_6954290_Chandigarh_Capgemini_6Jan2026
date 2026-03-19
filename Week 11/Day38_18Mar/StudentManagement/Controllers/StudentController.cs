using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{   
    public class StudentController : Controller
    {
		private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

		public IActionResult Index()

        {
            var AllStudent = _db.Students.ToList();
            return View(AllStudent);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Register(Student obj)
		{
            _db.Students.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
		}


		public IActionResult Edit( int id)
		{
			var student = _db.Students.Find(id);
			return View(student);
		}

		[HttpPost]
        public IActionResult Edit(Student obj)
        {
            _db.Students.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var student = _db.Students.Find(id);

            _db.Students.Remove(student);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
	}
}

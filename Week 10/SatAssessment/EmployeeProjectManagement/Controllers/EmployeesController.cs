using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeProjectManagement.Data;
using EmployeeProjectManagement.Models;

namespace EmployeeProjectManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.EmployeeProjects)
                .ThenInclude(ep => ep.Project)
                .ToList();

            return View(employees);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Projects = _context.Projects.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee, int[] projectIds)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            foreach (var pid in projectIds)
            {
                _context.EmployeeProjects.Add(new EmployeeProject
                {
                    EmployeeId = employee.EmployeeId,
                    ProjectId = pid,
                    AssignedDate = DateTime.Now
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
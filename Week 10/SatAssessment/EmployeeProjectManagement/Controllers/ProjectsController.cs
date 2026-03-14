using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeProjectManagement.Data;
using EmployeeProjectManagement.Models;

namespace EmployeeProjectManagement.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var projects = _context.Projects
                .Include(p => p.EmployeeProjects)
                .ThenInclude(ep => ep.Employee)
                .ToList();

            return View(projects);
        }

        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project, int[] employeeIds)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();

            foreach (var eid in employeeIds)
            {
                _context.EmployeeProjects.Add(new EmployeeProject
                {
                    EmployeeId = eid,
                    ProjectId = project.ProjectId,
                    AssignedDate = DateTime.Now
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
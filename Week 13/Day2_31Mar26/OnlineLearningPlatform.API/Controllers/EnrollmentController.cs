using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using OnlineLearningPlatform.API.Data;
using OnlineLearningPlatform.API.Models;

using AutoMapper;

namespace OnlineLearningPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EnrollmentController(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ===========================
    // STUDENT ENROLLED COURSES
    // ===========================

    [HttpGet("me")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> MyEnrollments()
    {
        var username = User.FindFirstValue(ClaimTypes.Name);

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            return Unauthorized();

        var courses = await _context.Enrollments
            .Include(e => e.Course)
            .Where(e => e.UserId == user.Id)
            .Select(e => e.Course)
            .ToListAsync();

        var courseDtos = _mapper.Map<List<CourseDto>>(courses);

        return Ok(courseDtos);
    }

    // ===========================
    // ENROLL COURSE (Student)
    // ===========================

    [HttpPost("{courseId}")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult>
        Enroll(int courseId)
    {
        var username =
            User.FindFirstValue(
                ClaimTypes.Name);

        var user =
            await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Username == username);

        if (user == null)
            return Unauthorized();

        var enrollment =
            new Enrollment
            {
                UserId = user.Id,
                CourseId = courseId,
                EnrolledDate = DateTime.Now
            };

        _context.Enrollments.Add(enrollment);

        await _context.SaveChangesAsync();

        return Ok(
            new { message = "Enrolled successfully" });
    }
}
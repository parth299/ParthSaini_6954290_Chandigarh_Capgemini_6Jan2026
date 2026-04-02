using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using OnlineLearningPlatform.API.Data;
using OnlineLearningPlatform.API.Models;
using OnlineLearningPlatform.API.DTOs;

namespace OnlineLearningPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public LessonController(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ===========================
    // ADD LESSON (Instructor)
    // ===========================

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<ActionResult<LessonDto>>
        CreateLesson(LessonDto dto)
    {
        var lesson =
            _mapper.Map<Lesson>(dto);

        _context.Lessons.Add(lesson);

        await _context.SaveChangesAsync();

        return Ok(
            _mapper.Map<LessonDto>(lesson));
    }

    // ===========================
    // GET LESSONS BY COURSE
    // ===========================

    [HttpGet("course/{courseId}")]
    public async Task<ActionResult<IEnumerable<LessonDto>>>
        GetLessonsByCourse(int courseId)
    {
        var lessons =
            await _context.Lessons
                .Where(l =>
                    l.CourseId == courseId)
                .ToListAsync();

        return Ok(
            _mapper.Map<List<LessonDto>>(lessons));
    }
}
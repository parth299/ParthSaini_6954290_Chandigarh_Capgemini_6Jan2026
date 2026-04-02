using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using OnlineLearningPlatform.API.Data;
using OnlineLearningPlatform.API.Models;
using OnlineLearningPlatform.API.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace OnlineLearningPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public CourseController(
        AppDbContext context,
        IMapper mapper,
        IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    // ===========================
    // GET ALL COURSES
    // ===========================

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>>
    GetCourses(
        int page = 1,
        int pageSize = 10,
        string? search = null)
    {
        string cacheKey =
            $"courses_{page}_{pageSize}_{search}";

        if (!_cache.TryGetValue(cacheKey,
            out List<CourseDto> cachedCourses))
        {
            var query =
                _context.Courses.AsQueryable();

            // 🔎 SEARCH

            if (!string.IsNullOrEmpty(search))
            {
                query =
                    query.Where(c =>
                        c.Title.Contains(search));
            }

            // 📄 PAGINATION

            var courses =
                await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            cachedCourses =
                _mapper.Map<List<CourseDto>>(courses);

            var cacheOptions =
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(
                        TimeSpan.FromMinutes(5));

            _cache.Set(
                cacheKey,
                cachedCourses,
                cacheOptions);
        }

        return Ok(cachedCourses);
    }

    // ===========================
    // GET COURSE BY ID
    // ===========================

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>>
        GetCourse(int id)
    {
        var course =
            await _context.Courses
                .FindAsync(id);

        if (course == null)
            return NotFound();

        return Ok(
            _mapper.Map<CourseDto>(course));
    }

    // ===========================
    // CREATE COURSE (Instructor)
    // ===========================

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<ActionResult<CourseDto>>
    CreateCourse(CourseDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var course =
            _mapper.Map<Course>(dto);

        _context.Courses.Add(course);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCourse),
            new { id = course.Id },
            _mapper.Map<CourseDto>(course));
    }

    // ===========================
    // UPDATE COURSE
    // ===========================

    [HttpPut("{id}")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult>
        UpdateCourse(int id, CourseDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        var course =
            await _context.Courses
                .FindAsync(id);

        if (course == null)
            return NotFound();

        _mapper.Map(dto, course);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ===========================
    // DELETE COURSE
    // ===========================

    [HttpDelete("{id}")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult>
        DeleteCourse(int id)
    {
        var course =
            await _context.Courses
                .FindAsync(id);

        if (course == null)
            return NotFound();

        _context.Courses.Remove(course);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
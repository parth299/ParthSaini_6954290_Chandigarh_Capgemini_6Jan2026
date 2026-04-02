using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineLearningPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("student")]
    [Authorize(Roles = "Student")]
    public IActionResult StudentOnly()
    {
        return Ok("Student Access Granted");
    }

    [HttpGet("instructor")]
    [Authorize(Roles = "Instructor")]
    public IActionResult InstructorOnly()
    {
        return Ok("Instructor Access Granted");
    }
}
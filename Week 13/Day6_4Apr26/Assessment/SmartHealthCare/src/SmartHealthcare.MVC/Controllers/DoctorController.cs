using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.MVC.Services;
using SmartHealthcare.Models.DTOs;

namespace SmartHealthcare.MVC.Controllers;

public class DoctorController : Controller
{
    private readonly IApiService _apiService;

    public DoctorController(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Profile()
    {
        var token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var doctor = await _apiService
            .GetAsync<DoctorDTO>("doctors/my-profile", token);

        if (doctor == null)
        {
            ViewBag.NeedsProfile = true;
            return View(new DoctorDTO());
        }

        return View(doctor);
    }

    public async Task<IActionResult> Index(string? specialization, string? name, int page = 1)
    {
        var token = HttpContext.Session.GetString("Token");
        var endpoint = $"doctors?pageNumber={page}&pageSize=20";

        if (!string.IsNullOrWhiteSpace(specialization))
        {
            endpoint = $"doctors/search?specialization={Uri.EscapeDataString(specialization)}&pageNumber={page}&pageSize=20";
        }

        var result = await _apiService.GetAsync<PagedResult<DoctorDTO>>(endpoint, token);
        ViewBag.Specialization = specialization;
        ViewBag.Name = name;
        return View(result ?? new PagedResult<DoctorDTO>());
    }

    public async Task<IActionResult> Details(int id)
    {
        var token = HttpContext.Session.GetString("Token");
        var doctor = await _apiService.GetAsync<DoctorDTO>($"doctors/{id}", token);
        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile(CreateDoctorDTO dto)
    {
        var token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.NeedsProfile = true;
            return View("Profile", new DoctorDTO());
        }

        var result = await _apiService
            .PostAsync<DoctorDTO>("doctors", dto, token);

        if (result == null)
        {
            ModelState.AddModelError(
                string.Empty,
                "Failed to create profile"
            );

            ViewBag.NeedsProfile = true;

            return View("Profile", new DoctorDTO());
        }

        TempData["Success"] =
            "Doctor profile created successfully!";

        return RedirectToAction(nameof(Profile));
    }
}
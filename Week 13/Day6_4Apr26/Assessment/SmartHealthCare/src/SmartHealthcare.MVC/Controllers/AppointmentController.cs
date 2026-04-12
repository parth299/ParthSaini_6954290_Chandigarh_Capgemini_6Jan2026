using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.MVC.Services;
using SmartHealthcare.Models.DTOs;

namespace SmartHealthcare.MVC.Controllers;

public class AppointmentController : Controller
{
    private readonly IApiService _apiService;
    private readonly ILogger<AppointmentController> _logger;

    public AppointmentController(IApiService apiService, ILogger<AppointmentController> logger)
    {
        _apiService = apiService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var role = HttpContext.Session.GetString("Role");
        PagedResult<AppointmentDTO>? result = null;

        if (role == "Patient")
        {
            result = await _apiService.GetAsync<PagedResult<AppointmentDTO>>("appointments/my-appointments?pageNumber=1&pageSize=50", token);
        }
        else if (role == "Doctor")
        {
            result = await _apiService.GetAsync<PagedResult<AppointmentDTO>>("appointments/doctor-appointments?pageNumber=1&pageSize=50", token);
        }
        else if (role == "Admin")
        {
            result = await _apiService.GetAsync<PagedResult<AppointmentDTO>>("appointments?pageNumber=1&pageSize=50", token);
        }

        ViewBag.Role = role;
        return View(result?.Items ?? Enumerable.Empty<AppointmentDTO>());
    }

    public async Task<IActionResult> Create()
    {
        var token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await _apiService
            .GetAsync<PagedResult<DoctorDTO>>(
                "doctors?pageNumber=1&pageSize=100",
                token
            );

        ViewBag.Doctors = result?.Items;

        return View(new CreateAppointmentDTO());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentDTO dto)
    {
        var token = HttpContext.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Doctors = await _apiService.GetAsync<IEnumerable<DoctorDTO>>("doctors", token) ?? Enumerable.Empty<DoctorDTO>();
            return View(dto);
        }

        var result = await _apiService.PostAsync<AppointmentDTO>("appointments", dto, token);
        if (result == null)
        {
            ModelState.AddModelError(string.Empty, "Failed to create appointment. Please ensure your patient profile is set up.");
            ViewBag.Doctors = await _apiService.GetAsync<IEnumerable<DoctorDTO>>("doctors", token) ?? Enumerable.Empty<DoctorDTO>();
            return View(dto);
        }

        _logger.LogInformation("Appointment booked successfully");
        TempData["Success"] = "Appointment booked successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        // Load appointment
        var appointment = await _apiService
            .GetAsync<AppointmentDTO>(
                $"appointments/{id}",
                token
            );

        if (appointment == null)
        {
            return NotFound();
        }

        // Load prescription (VERY IMPORTANT)

        var prescription = await _apiService
            .GetAsync<PrescriptionDTO>(
                $"prescriptions/appointment/{id}",
                token
            );

        ViewBag.Prescription = prescription;

        return View(appointment);
    }
}
using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.MVC.Services;
using SmartHealthcare.Models.DTOs;

namespace SmartHealthcare.MVC.Controllers;

public class PrescriptionController : Controller
{
    private readonly IApiService _apiService;

    public PrescriptionController(IApiService apiService)
    {
        _apiService = apiService;
    }

    // =========================
    // GET: Create Prescription
    // =========================

    public IActionResult Create(int appointmentId)
    {
        var model = new CreatePrescriptionDTO
        {
            AppointmentId = appointmentId,
            Medicines = new List<CreateMedicineDTO>()
        };

        return View(model);
    }

    // =========================
    // POST: Create Prescription
    // =========================

    [HttpPost]
    public async Task<IActionResult> Create(CreatePrescriptionDTO dto)
    {
        var token = HttpContext.Session.GetString("Token");

        if (!ModelState.IsValid)
            return View(dto);

        var result = await _apiService
            .PostAsync<PrescriptionDTO>(
                "prescriptions",
                dto,
                token
            );

        if (result == null)
        {
            ModelState.AddModelError(
                "",
                "Failed to create prescription"
            );

            return View(dto);
        }

        TempData["Success"] =
            "Prescription created successfully!";

        return RedirectToAction(
            "Details",
            "Appointment",
            new { id = dto.AppointmentId }
        );
    }
}
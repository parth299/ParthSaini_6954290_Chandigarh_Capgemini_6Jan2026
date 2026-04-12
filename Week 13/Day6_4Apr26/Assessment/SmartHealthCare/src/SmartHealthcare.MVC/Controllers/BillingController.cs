using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.MVC.Services;
using SmartHealthcare.Models.DTOs;
using SmartHealthcare.MVC.Models.ViewModels;

namespace SmartHealthcare.MVC.Controllers;

public class BillingController : Controller
{
    private readonly IApiService _apiService;

    public BillingController(IApiService apiService)
    {
        _apiService = apiService;
    }

    // =========================
    // Generate Bill Page
    // =========================

    public async Task<IActionResult> Generate(int appointmentId)
    {
        var token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        // Get Appointment

        var appointment = await _apiService
            .GetAsync<AppointmentDTO>(
                $"appointments/{appointmentId}",
                token
            );

        if (appointment == null)
        {
            return NotFound();
        }

        // Get Prescription

        var prescription = await _apiService
            .GetAsync<PrescriptionDTO>(
                $"prescriptions/appointment/{appointmentId}",
                token
            );

        if (prescription == null)
        {
            TempData["Error"] =
                "No prescription available.";

            return RedirectToAction(
                "Details",
                "Appointment",
                new { id = appointmentId }
            );
        }

        // Get Doctor

        var doctor = await _apiService
            .GetAsync<DoctorDTO>(
                $"doctors/{appointment.DoctorId}",
                token
            );

        // Create ViewModel

        var model = new BillingViewModel
        {
            Appointment = appointment,
            Prescription = prescription,
            Doctor = doctor
        };

        return View(model);
    }
}
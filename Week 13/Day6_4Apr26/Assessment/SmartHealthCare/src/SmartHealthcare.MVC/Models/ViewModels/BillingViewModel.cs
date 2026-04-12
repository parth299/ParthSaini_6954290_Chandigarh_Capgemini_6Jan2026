using SmartHealthcare.Models.DTOs;

namespace SmartHealthcare.MVC.Models.ViewModels;

public class BillingViewModel
{
    public AppointmentDTO Appointment { get; set; }

    public PrescriptionDTO Prescription { get; set; }

    public DoctorDTO Doctor { get; set; }

    public decimal ConsultationFee =>
        Doctor?.ConsultationFee ?? 0;

    public decimal MedicineTotal =>
        Prescription?.Medicines?.Count * 100 ?? 0;

    public decimal Total =>
        ConsultationFee + MedicineTotal;
}
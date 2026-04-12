using HealthcareSystem.Domain.Common;

namespace HealthcareSystem.Domain.Entities;

public class Doctor : BaseEntity
{
    public string FullName { get; set; }

    public string Qualification { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public ICollection<Appointment> Appointments { get; set; }

    public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; }
}
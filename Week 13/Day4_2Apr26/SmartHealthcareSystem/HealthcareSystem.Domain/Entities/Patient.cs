using HealthcareSystem.Domain.Common;

namespace HealthcareSystem.Domain.Entities;

public class Patient : BaseEntity
{
    public string FullName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Phone { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}
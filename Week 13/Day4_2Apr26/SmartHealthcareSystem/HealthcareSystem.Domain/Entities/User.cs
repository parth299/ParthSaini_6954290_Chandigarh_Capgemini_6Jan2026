using HealthcareSystem.Domain.Common;

namespace HealthcareSystem.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public Patient Patient { get; set; }

    public Doctor Doctor { get; set; }
}
using Microsoft.EntityFrameworkCore;
using HealthcareSystem.Domain.Entities;

namespace HealthcareSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<Prescription> Prescriptions { get; set; }

    public DbSet<Medicine> Medicines { get; set; }

    public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
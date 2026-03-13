using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Student
{
    [Key]
    public int StudentId { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public string Email { get; set; }

    public DateTime EnrollmentDate { get; set; }

    // Navigation Property
    public ICollection<Enrollment> Enrollments { get; set; }
}
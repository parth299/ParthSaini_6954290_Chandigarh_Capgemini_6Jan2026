using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Course
{
    [Key]
    public int CourseId { get; set; }

    [Required]
    public string Title { get; set; }

    public int Credits { get; set; }

    // Foreign Key
    public int InstructorId { get; set; }

    // Navigation Properties
    public Instructor Instructor { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
}
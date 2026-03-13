using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Instructor
{
    [Key]
    public int InstructorId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Department { get; set; }

    // Foreign Key
    public int DepartmentId { get; set; }

    // Navigation Properties
    public Department DepartmentObj { get; set; }

    public ICollection<Course> Courses { get; set; }
}
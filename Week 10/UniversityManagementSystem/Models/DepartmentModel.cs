using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    public string Name { get; set; }

    public decimal Budget { get; set; }

    // Navigation Property
    public ICollection<Instructor> Instructors { get; set; }
}
using System.ComponentModel.DataAnnotations;

public class CourseDto
{
    public int Id { get; set; }

    [Required(ErrorMessage =
        "Title is required")]
    public string Title { get; set; }

    public string Description { get; set; }
}
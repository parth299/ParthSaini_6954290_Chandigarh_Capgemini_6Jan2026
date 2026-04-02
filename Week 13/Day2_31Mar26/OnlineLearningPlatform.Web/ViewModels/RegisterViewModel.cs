using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.Web.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }
}
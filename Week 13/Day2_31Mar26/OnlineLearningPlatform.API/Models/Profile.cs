namespace OnlineLearningPlatform.API.Models;

public class Profile
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Bio { get; set; }

    // Foreign Key
    public int UserId { get; set; }

    public User User { get; set; }
}
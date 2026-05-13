namespace BookStore.Domain.Entities;

public class UserProfile
{
    public int ProfileId { get; set; }
    public int UserId { get; set; }
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Pincode { get; set; } = null!;

    // Navigation properties
    public User? User { get; set; }
}

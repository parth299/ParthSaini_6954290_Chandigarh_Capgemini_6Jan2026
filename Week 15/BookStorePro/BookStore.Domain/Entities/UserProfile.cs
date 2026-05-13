namespace BookStore.Domain.Entities;

public class UserProfile : BaseEntity
{
    public int UserId { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Pincode { get; set; }

    // Navigation
    public User User { get; set; }
}
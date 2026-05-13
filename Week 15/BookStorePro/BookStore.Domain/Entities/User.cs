namespace BookStore.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Phone { get; set; }

    public int RoleId { get; set; }

    // Navigation
    public Role Role { get; set; }

    public UserProfile UserProfile { get; set; }

    public ICollection<Order> Orders { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<Wishlist> Wishlists { get; set; }
}
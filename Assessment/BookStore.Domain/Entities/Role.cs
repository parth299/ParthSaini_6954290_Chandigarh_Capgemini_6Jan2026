namespace BookStore.Domain.Entities;

public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;

    // Navigation properties
    public ICollection<User> Users { get; set; } = new List<User>();
}

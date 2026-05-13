namespace BookStore.Domain.Entities;

public class Role : BaseEntity
{
    public string RoleName { get; set; }

    // Navigation
    public ICollection<User> Users { get; set; }
}
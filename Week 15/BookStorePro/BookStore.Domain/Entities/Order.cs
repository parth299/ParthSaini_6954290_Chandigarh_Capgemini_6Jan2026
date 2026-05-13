namespace BookStore.Domain.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; }

    // Navigation
    public User User { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}
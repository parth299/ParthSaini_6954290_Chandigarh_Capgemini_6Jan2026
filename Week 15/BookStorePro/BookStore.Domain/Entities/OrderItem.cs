namespace BookStore.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }

    public int BookId { get; set; }

    public int Qty { get; set; }

    public decimal Price { get; set; }

    // Navigation
    public Order Order { get; set; }

    public Book Book { get; set; }
}
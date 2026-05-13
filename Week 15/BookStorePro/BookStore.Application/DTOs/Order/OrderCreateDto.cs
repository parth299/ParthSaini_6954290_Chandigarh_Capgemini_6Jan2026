namespace BookStore.Application.DTOs.Order;

public class OrderCreateDto
{
    public int UserId { get; set; }

    public List<OrderItemDto> Items { get; set; }
}

public class OrderItemDto
{
    public int BookId { get; set; }

    public int Qty { get; set; }
}
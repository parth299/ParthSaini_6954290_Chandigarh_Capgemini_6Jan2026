namespace BookStore.Application.DTOs;

public class OrderCreateDto
{
    public int UserId { get; set; }
    public List<OrderItemCreateDto> OrderItems { get; set; } = new();
}

public class OrderItemCreateDto
{
    public int BookId { get; set; }
    public int Qty { get; set; }
}

public class OrderResponseDto
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = null!;
    public List<OrderItemResponseDto> OrderItems { get; set; } = new();
}

public class OrderItemResponseDto
{
    public int OrderItemId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = null!;
    public int Qty { get; set; }
    public decimal Price { get; set; }
}

public class OrderStatusUpdateDto
{
    public int OrderId { get; set; }
    public string Status { get; set; } = null!;
}

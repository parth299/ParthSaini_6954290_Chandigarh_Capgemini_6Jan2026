namespace BookStore.Application.DTOs.Order;

public class OrderResponseDto
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; }

    public List<OrderItemResponseDto> Items { get; set; }
}

public class OrderItemResponseDto
{
    public string BookTitle { get; set; }

    public int Qty { get; set; }

    public decimal Price { get; set; }
}
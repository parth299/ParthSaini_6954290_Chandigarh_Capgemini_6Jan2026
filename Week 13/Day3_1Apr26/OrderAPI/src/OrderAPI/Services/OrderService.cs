using OrderAPI.Models;

namespace OrderAPI.Services;

/// <summary>
/// In-memory implementation of IOrderService.
/// Returns false if order is invalid (e.g. zero quantity or negative amount).
/// </summary>
public class OrderService : IOrderService
{
    private static readonly List<Order> _orders = new();

    public Task<bool> PlaceOrderAsync(Order order)
    {
        // Basic validation — invalid orders return false (400 Bad Request)
        if (order.Quantity <= 0 || order.TotalAmount <= 0 || string.IsNullOrWhiteSpace(order.ProductName))
            return Task.FromResult(false);

        order.Id     = _orders.Count + 1;
        order.Status = "Confirmed";
        _orders.Add(order);

        return Task.FromResult(true);
    }
}

using OrderAPI.Models;

namespace OrderAPI.Services;

public interface IOrderService
{
    Task<bool> PlaceOrderAsync(Order order);
}

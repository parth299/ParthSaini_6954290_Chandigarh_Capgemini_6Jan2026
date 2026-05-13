using BookStore.Application.DTOs.Order;

namespace BookStore.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponseDto>
        PlaceOrderAsync(
            OrderCreateDto dto);

    Task<OrderResponseDto>
        GetOrderAsync(int orderId);
}
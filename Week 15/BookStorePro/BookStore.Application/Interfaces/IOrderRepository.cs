using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order> GetOrderWithItemsAsync(int orderId);
}

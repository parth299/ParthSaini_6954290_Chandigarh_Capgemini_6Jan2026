using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using BookStore.Application.Interfaces;

namespace BookStore.Infrastructure.Repositories;

public class OrderRepository
    : GenericRepository<Order>,
      IOrderRepository
{
    public OrderRepository(
        ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<Order>
        GetOrderWithItemsAsync(int orderId)
    {
        return await _context.Orders
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Book)
            .FirstOrDefaultAsync(
                x => x.Id == orderId);
    }
}
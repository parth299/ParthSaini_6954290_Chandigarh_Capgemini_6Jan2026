using BookStore.Infrastructure.Data;
using BookStore.Application.Interfaces;

namespace BookStore.Infrastructure.Repositories;

public class UnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IBookRepository Books { get; }

    public IOrderRepository Orders { get; }

    public UnitOfWork(
        ApplicationDbContext context)
    {
        _context = context;

        Books = new BookRepository(context);

        Orders = new OrderRepository(context);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
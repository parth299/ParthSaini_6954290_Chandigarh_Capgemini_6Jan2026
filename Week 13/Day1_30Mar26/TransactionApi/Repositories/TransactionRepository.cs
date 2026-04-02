using Microsoft.EntityFrameworkCore;
using TransactionApi.Data;
using TransactionApi.Models;
using TransactionApi.Helpers;

namespace TransactionApi.Repositories
{
    public class TransactionRepository
        : Repository<Transaction>,
          ITransactionRepository
    {
        public TransactionRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Transaction>>
            GetUserTransactionsAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task AddTransactionAsync(
    Transaction transaction)
{
    await _context.Transactions
        .AddAsync(transaction);

    await _context.SaveChangesAsync();
}

        public async Task<IEnumerable<Transaction>>
    GetUserTransactionsAsync(
        int userId,
        QueryParameters queryParams)
{
    var query =
        _context.Transactions
        .Where(t => t.UserId == userId)
        .AsQueryable();

    // Filtering

    if (!string.IsNullOrEmpty(queryParams.Type))
    {
        query =
            query.Where(t =>
                t.Type == queryParams.Type);
    }

    // Sorting

    if (!string.IsNullOrEmpty(queryParams.SortBy))
    {
        switch (queryParams.SortBy.ToLower())
        {
            case "amount":
                query =
                    query.OrderBy(t => t.Amount);
                break;

            case "date":
                query =
                    query.OrderByDescending(
                        t => t.Date);
                break;

            default:
                query =
                    query.OrderByDescending(
                        t => t.Date);
                break;
        }
    }
    else
    {
        query =
            query.OrderByDescending(
                t => t.Date);
    }

    // Pagination

    query =
        query.Skip(
            (queryParams.PageNumber - 1)
            * queryParams.PageSize)
        .Take(queryParams.PageSize);

    return await query.ToListAsync();
}
    }
}
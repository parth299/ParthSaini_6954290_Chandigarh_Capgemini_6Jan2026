using TransactionApi.Models;
using TransactionApi.Helpers;

namespace TransactionApi.Repositories
{
    public interface ITransactionRepository
        : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>>
            GetUserTransactionsAsync(int userId);

        Task<IEnumerable<Transaction>>
            GetUserTransactionsAsync(
                int userId,
                QueryParameters queryParams);
            
        Task AddTransactionAsync(Transaction transaction);
    }
}
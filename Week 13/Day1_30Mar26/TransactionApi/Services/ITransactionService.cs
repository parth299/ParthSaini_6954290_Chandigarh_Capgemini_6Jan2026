using TransactionApi.DTOs;
using TransactionApi.Helpers;

namespace TransactionApi.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>>
            GetUserTransactionsAsync(int userId);

        Task<IEnumerable<TransactionDto>>
            GetUserTransactionsAsync(
                int userId,
                QueryParameters queryParams);

        Task<TransactionDto> CreateTransactionAsync(
    int userId,
    CreateTransactionDto dto);
    }
}
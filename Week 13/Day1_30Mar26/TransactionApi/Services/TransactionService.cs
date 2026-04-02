using AutoMapper;
using TransactionApi.DTOs;
using TransactionApi.Repositories;
using TransactionApi.Helpers;
using TransactionApi.Models;

namespace TransactionApi.Services
{
    public class TransactionService
        : ITransactionService
    {
        private readonly
            ITransactionRepository _repository;

        private readonly IMapper _mapper;

        public TransactionService(
            ITransactionRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // OLD METHOD (without pagination)

        public async Task<IEnumerable<TransactionDto>>
            GetUserTransactionsAsync(int userId)
        {
            var transactions =
                await _repository
                    .GetUserTransactionsAsync(userId);

            return _mapper.Map<
                IEnumerable<TransactionDto>>(
                transactions);
        }

        // NEW METHOD (with pagination)

        public async Task<IEnumerable<TransactionDto>>
            GetUserTransactionsAsync(
                int userId,
                QueryParameters queryParams)
        {
            var transactions =
                await _repository
                    .GetUserTransactionsAsync(
                        userId,
                        queryParams);

            return _mapper.Map<
                IEnumerable<TransactionDto>>(
                transactions);
        }

        public async Task<TransactionDto>
    CreateTransactionAsync(
        int userId,
        CreateTransactionDto dto)
{
    var transaction =
        _mapper.Map<Transaction>(dto);

    transaction.UserId = userId;

    transaction.Date = DateTime.UtcNow;

    await _repository
        .AddTransactionAsync(transaction);

    return _mapper.Map<
        TransactionDto>(transaction);
}
    }
}
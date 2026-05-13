namespace BookStore.Application.Interfaces;

public interface IPaymentService
{
    Task<bool>
        ProcessPaymentAsync(
            decimal amount);
}
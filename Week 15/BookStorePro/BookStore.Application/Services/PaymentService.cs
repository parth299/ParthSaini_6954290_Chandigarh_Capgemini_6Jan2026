using BookStore.Application.Interfaces;

namespace BookStore.Application.Services;

public class PaymentService
    : IPaymentService
{
    public async Task<bool>
        ProcessPaymentAsync(
            decimal amount)
    {
        await Task.Delay(500);

        return true;
    }
}
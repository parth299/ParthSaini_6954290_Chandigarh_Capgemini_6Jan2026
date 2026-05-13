using BookStore.Application.Interfaces;

namespace BookStore.Application.Services;

public class EmailService : IEmailService
{
    // In a production environment, integrate with SendGrid, AWS SES, or SMTP
    // For now, logging to console
    public async Task SendOrderConfirmationAsync(string email, int orderId)
    {
        await Task.Run(() =>
        {
            Console.WriteLine($"[EMAIL] Order Confirmation sent to {email} for Order ID: {orderId}");
            // TODO: Implement actual email sending
        });
    }

    public async Task SendOrderInvoiceAsync(string email, int orderId)
    {
        await Task.Run(() =>
        {
            Console.WriteLine($"[EMAIL] Order Invoice sent to {email} for Order ID: {orderId}");
            // TODO: Implement actual invoice generation and email sending
        });
    }

    public async Task SendLowStockAlertAsync(string adminEmail, string bookTitle, int stock)
    {
        await Task.Run(() =>
        {
            Console.WriteLine($"[EMAIL] Low stock alert sent to {adminEmail} for book: {bookTitle}, Current stock: {stock}");
            // TODO: Implement actual email sending to admin
        });
    }
}

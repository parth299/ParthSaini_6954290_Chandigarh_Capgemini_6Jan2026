using BookStore.Web.Models.DTOs;

namespace BookStore.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItemDto> CartItems { get; set; } = new();
        public decimal TotalAmount => CartItems.Sum(item => item.Price * item.Quantity);
        
        // Shipping Information
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        
        // Payment Information
        public string? CardNumber { get; set; }
        public string? ExpiryMonth { get; set; }
        public string? ExpiryYear { get; set; }
        public string? CardHolderName { get; set; }
        public string? CVV { get; set; }
    }
}

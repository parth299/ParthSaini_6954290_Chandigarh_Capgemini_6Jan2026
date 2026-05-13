using BookStore.Web.Models.DTOs;

namespace BookStore.Web.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemDto> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(item => item.Price * item.Quantity);
    }
}

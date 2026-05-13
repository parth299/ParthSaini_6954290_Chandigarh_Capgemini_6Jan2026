using BookStore.Web.Models.DTOs;

namespace BookStore.Web.ViewModels
{
    public class OrderConfirmationViewModel
    {
        public OrderDto Order { get; set; } = new();
    }
}

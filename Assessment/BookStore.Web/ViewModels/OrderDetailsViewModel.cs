using BookStore.Web.Models.DTOs;

namespace BookStore.Web.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDto Order { get; set; } = new();
    }
}

using BookStore.Web.Models.DTOs;

namespace BookStore.Web.ViewModels
{
    public class OrderListViewModel
    {
        public List<OrderDto> Orders { get; set; } = new();
    }
}

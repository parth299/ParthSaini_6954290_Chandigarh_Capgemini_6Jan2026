using BookStore.Web.Models.DTOs;

namespace BookStore.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<BookDto> FeaturedBooks { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
    }
}

using BookStore.Web.Models.DTOs;

namespace BookStore.Web.ViewModels
{
    public class BookListViewModel
    {
        public List<BookDto> Books { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
        public string? SearchKeyword { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 12;
    }
}

using BookStore.Web.Models.DTOs;

namespace BookStore.Web.ViewModels
{
    public class BookDetailsViewModel
    {
        public BookDto Book { get; set; } = new();
        public List<ReviewDto> Reviews { get; set; } = new();
    }
}

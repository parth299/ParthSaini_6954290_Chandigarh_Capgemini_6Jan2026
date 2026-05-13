using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(UserLoginDto dto);
    Task<AuthResponseDto> RegisterAsync(UserRegisterDto dto);
}

public interface ITokenService
{
    string GenerateToken(int userId, string email, int roleId, string roleName);
}

public interface IEmailService
{
    Task SendOrderConfirmationAsync(string email, int orderId);
    Task SendOrderInvoiceAsync(string email, int orderId);
    Task SendLowStockAlertAsync(string adminEmail, string bookTitle, int stock);
}

public interface IBookService
{
    Task<BookDto?> GetBookByIdAsync(int bookId);
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId);
    Task<IEnumerable<BookDto>> SearchBooksAsync(string keyword);
    Task<BookDto> CreateBookAsync(BookCreateDto dto);
    Task UpdateBookAsync(BookUpdateDto dto);
    Task DeleteBookAsync(int bookId);
}

public interface IOrderService
{
    Task<OrderResponseDto?> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<OrderResponseDto>> GetUserOrdersAsync(int userId);
    Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto dto);
    Task UpdateOrderStatusAsync(OrderStatusUpdateDto dto);
}

public interface IReviewService
{
    Task<ReviewDto?> GetReviewByIdAsync(int reviewId);
    Task<IEnumerable<ReviewDto>> GetBookReviewsAsync(int bookId);
    Task<ReviewDto> CreateReviewAsync(int userId, ReviewCreateDto dto);
    Task DeleteReviewAsync(int reviewId);
}

public interface IWishlistService
{
    Task<IEnumerable<WishlistDto>> GetUserWishlistAsync(int userId);
    Task AddToWishlistAsync(int userId, WishlistCreateDto dto);
    Task RemoveFromWishlistAsync(int userId, int bookId);
}

public interface ICategoryService
{
    Task<CategoryDto?> GetCategoryByIdAsync(int categoryId);
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto dto);
    Task UpdateCategoryAsync(int categoryId, CategoryCreateDto dto);
    Task DeleteCategoryAsync(int categoryId);
}

public interface IAuthorService
{
    Task<AuthorDto?> GetAuthorByIdAsync(int authorId);
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
    Task<AuthorDto> CreateAuthorAsync(AuthorCreateDto dto);
    Task UpdateAuthorAsync(int authorId, AuthorCreateDto dto);
    Task DeleteAuthorAsync(int authorId);
}

public interface IPublisherService
{
    Task<PublisherDto?> GetPublisherByIdAsync(int publisherId);
    Task<IEnumerable<PublisherDto>> GetAllPublishersAsync();
    Task<PublisherDto> CreatePublisherAsync(PublisherCreateDto dto);
    Task UpdatePublisherAsync(int publisherId, PublisherCreateDto dto);
    Task DeletePublisherAsync(int publisherId);
}

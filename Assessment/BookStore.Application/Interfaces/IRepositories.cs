using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetWithProfileAsync(int userId);
}

public interface IBookRepository : IGenericRepository<Book>
{
    Task<IEnumerable<Book>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Book>> GetByAuthorAsync(int authorId);
    Task<Book?> GetByIsbnAsync(string isbn);
    Task<IEnumerable<Book>> SearchBooksAsync(string keyword);
}

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
    Task<Order?> GetOrderWithItemsAsync(int orderId);
}

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
}

public interface IAuthorRepository : IGenericRepository<Author>
{
    Task<Author?> GetByNameAsync(string name);
}

public interface IPublisherRepository : IGenericRepository<Publisher>
{
    Task<Publisher?> GetByNameAsync(string name);
}

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<IEnumerable<Review>> GetBookReviewsAsync(int bookId);
    Task<IEnumerable<Review>> GetUserReviewsAsync(int userId);
    Task<decimal> GetBookAverageRatingAsync(int bookId);
}

public interface IWishlistRepository
{
    Task<IEnumerable<Wishlist>> GetUserWishlistAsync(int userId);
    Task<bool> IsInWishlistAsync(int userId, int bookId);
    Task AddToWishlistAsync(int userId, int bookId);
    Task RemoveFromWishlistAsync(int userId, int bookId);
}

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByNameAsync(string roleName);
}

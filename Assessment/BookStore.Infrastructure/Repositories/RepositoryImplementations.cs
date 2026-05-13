using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(BookStoreDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetWithProfileAsync(int userId)
    {
        return await _dbSet.Include(u => u.UserProfile).FirstOrDefaultAsync(u => u.UserId == userId);
    }
}

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(BookStoreDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Book>> GetByCategoryAsync(int categoryId)
    {
        return await _dbSet.Where(b => b.CategoryId == categoryId).ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetByAuthorAsync(int authorId)
    {
        return await _dbSet.Where(b => b.AuthorId == authorId).ToListAsync();
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        return await _dbSet.FirstOrDefaultAsync(b => b.ISBN == isbn);
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(string keyword)
    {
        return await _dbSet
            .Where(b => b.Title.Contains(keyword) || b.ISBN.Contains(keyword))
            .ToListAsync();
    }
}

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(BookStoreDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
    {
        return await _dbSet.Where(o => o.UserId == userId).ToListAsync();
    }

    public async Task<Order?> GetOrderWithItemsAsync(int orderId)
    {
        return await _dbSet.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == orderId);
    }
}

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BookStoreDbContext context) : base(context)
    {
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }
}

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
    public AuthorRepository(BookStoreDbContext context) : base(context)
    {
    }

    public async Task<Author?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.Name == name);
    }
}

public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
{
    public PublisherRepository(BookStoreDbContext context) : base(context)
    {
    }

    public async Task<Publisher?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Name == name);
    }
}

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(BookStoreDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Review>> GetBookReviewsAsync(int bookId)
    {
        return await _dbSet.Where(r => r.BookId == bookId).ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetUserReviewsAsync(int userId)
    {
        return await _dbSet.Where(r => r.UserId == userId).ToListAsync();
    }

    public async Task<decimal> GetBookAverageRatingAsync(int bookId)
    {
        var reviews = await GetBookReviewsAsync(bookId);
        return reviews.Any() ? (decimal)reviews.Average(r => r.Rating) : 0;
    }
}

public class WishlistRepository : IWishlistRepository
{
    private readonly BookStoreDbContext _context;

    public WishlistRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Wishlist>> GetUserWishlistAsync(int userId)
    {
        return await _context.Wishlists
            .Where(w => w.UserId == userId)
            .Include(w => w.Book)
            .ToListAsync();
    }

    public async Task<bool> IsInWishlistAsync(int userId, int bookId)
    {
        return await _context.Wishlists.AnyAsync(w => w.UserId == userId && w.BookId == bookId);
    }

    public async Task AddToWishlistAsync(int userId, int bookId)
    {
        if (!await IsInWishlistAsync(userId, bookId))
        {
            await _context.Wishlists.AddAsync(new Wishlist { UserId = userId, BookId = bookId });
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFromWishlistAsync(int userId, int bookId)
    {
        var wishlist = await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.BookId == bookId);
        if (wishlist != null)
        {
            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
        }
    }
}

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(BookStoreDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string roleName)
    {
        return await _dbSet.FirstOrDefaultAsync(r => r.RoleName == roleName);
    }
}

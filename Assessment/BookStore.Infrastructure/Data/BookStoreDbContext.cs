using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<EmailLog> EmailLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Role Configuration
        modelBuilder.Entity<Role>()
            .HasKey(r => r.RoleId);

        modelBuilder.Entity<Role>()
            .Property(r => r.RoleName)
            .IsRequired()
            .HasMaxLength(50);

        // User Configuration
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<User>()
            .Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Phone)
            .IsRequired()
            .HasMaxLength(15);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // UserProfile Configuration
        modelBuilder.Entity<UserProfile>()
            .HasKey(up => up.ProfileId);

        modelBuilder.Entity<UserProfile>()
            .Property(up => up.Address)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<UserProfile>()
            .Property(up => up.City)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<UserProfile>()
            .Property(up => up.Pincode)
            .IsRequired()
            .HasMaxLength(6);

        modelBuilder.Entity<UserProfile>()
            .HasOne(up => up.User)
            .WithOne(u => u.UserProfile)
            .HasForeignKey<UserProfile>(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserProfile>()
            .HasIndex(up => up.UserId)
            .IsUnique();

        // Category Configuration
        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryId);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Author Configuration
        modelBuilder.Entity<Author>()
            .HasKey(a => a.AuthorId);

        modelBuilder.Entity<Author>()
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Publisher Configuration
        modelBuilder.Entity<Publisher>()
            .HasKey(p => p.PublisherId);

        modelBuilder.Entity<Publisher>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Book Configuration
        modelBuilder.Entity<Book>()
            .HasKey(b => b.BookId);

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Book>()
            .Property(b => b.ISBN)
            .IsRequired()
            .HasMaxLength(20);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.ISBN)
            .IsUnique();

        modelBuilder.Entity<Book>()
            .Property(b => b.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.Restrict);

        // Order Configuration
        modelBuilder.Entity<Order>()
            .HasKey(o => o.OrderId);

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // OrderItem Configuration
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => oi.OrderItemId);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Book)
            .WithMany(b => b.OrderItems)
            .HasForeignKey(oi => oi.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        // Review Configuration
        modelBuilder.Entity<Review>()
            .HasKey(r => r.ReviewId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        // Wishlist Configuration (Many-to-Many)
        modelBuilder.Entity<Wishlist>()
            .HasKey(w => new { w.UserId, w.BookId });

        modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.User)
            .WithMany(u => u.Wishlists)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.Book)
            .WithMany(b => b.Wishlists)
            .HasForeignKey(w => w.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        // EmailLog Configuration
        modelBuilder.Entity<EmailLog>()
            .HasKey(el => el.EmailLogId);

        modelBuilder.Entity<EmailLog>()
            .Property(el => el.ToEmail)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<EmailLog>()
            .Property(el => el.Subject)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<EmailLog>()
            .Property(el => el.Status)
            .IsRequired()
            .HasMaxLength(50);
    }
}

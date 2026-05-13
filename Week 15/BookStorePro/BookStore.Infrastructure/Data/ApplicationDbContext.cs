using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Tables

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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply Fluent Configurations

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}
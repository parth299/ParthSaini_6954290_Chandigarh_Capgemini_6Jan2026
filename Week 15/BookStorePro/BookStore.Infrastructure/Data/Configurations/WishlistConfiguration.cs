using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Data.Configurations;

public class WishlistConfiguration
    : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(
        EntityTypeBuilder<Wishlist> builder)
    {
        builder.ToTable("Wishlist");

        // Composite Key
        builder.HasKey(x =>
            new { x.UserId, x.BookId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.Wishlists)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.Wishlists)
            .HasForeignKey(x => x.BookId);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Data.Configurations;

public class BookConfiguration
    : IEntityTypeConfiguration<Book>
{
    public void Configure(
        EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.ISBN)
            .HasMaxLength(20);

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500);

        // Category
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.CategoryId);

        // Author
        builder.HasOne(x => x.Author)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.AuthorId);

        // Publisher
        builder.HasOne(x => x.Publisher)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.PublisherId);
    }
}
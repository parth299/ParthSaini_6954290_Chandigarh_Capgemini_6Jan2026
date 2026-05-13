using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Data.Configurations;

public class OrderItemConfiguration
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(
        EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.BookId);
    }
}
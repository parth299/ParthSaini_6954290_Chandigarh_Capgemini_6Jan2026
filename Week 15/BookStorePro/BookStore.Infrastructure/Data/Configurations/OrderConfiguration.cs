using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Data.Configurations;

public class OrderConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(
        EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasMaxLength(50);

        builder.Property(x => x.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Data.Configurations;

public class UserProfileConfiguration
    : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(
        EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfile");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Address)
            .HasMaxLength(200);

        builder.Property(x => x.City)
            .HasMaxLength(50);

        builder.Property(x => x.Pincode)
            .HasMaxLength(10);

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}
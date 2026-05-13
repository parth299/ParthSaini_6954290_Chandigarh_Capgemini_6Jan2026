using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Data.Configurations;

public class UserConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(
        EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasMaxLength(15);

        // One-to-One
        builder.HasOne(x => x.UserProfile)
            .WithOne(x => x.User)
            .HasForeignKey<UserProfile>(
                x => x.UserId);
    }
}
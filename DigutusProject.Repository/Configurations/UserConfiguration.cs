using DigutusProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigutusProject.Repository.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(200);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.FirstName).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength(200);
        builder.Property(u => u.LastName).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(200);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(500)");
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnType("VARBINARY(500)");
        builder.Property(u => u.Role).IsRequired();
        builder.Property(u => u.CreateDate).IsRequired();
    }
}
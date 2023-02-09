using DigutusProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigutusProject.Repository.Configurations;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.CreateDate).IsRequired();
        builder.Property(l => l.IsLogin).IsRequired();
        builder.Property(l => l.Email).IsRequired();
    }
}
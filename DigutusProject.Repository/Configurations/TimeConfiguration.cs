using DigutusProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigutusProject.Repository.Configurations;

public class TimeConfiguration : IEntityTypeConfiguration<Time>
{
    public void Configure(EntityTypeBuilder<Time> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.CreateDate).IsRequired();
        builder.Property(l => l.StartTime).IsRequired();
        builder.Property(l => l.EndTime).IsRequired();
    }
}
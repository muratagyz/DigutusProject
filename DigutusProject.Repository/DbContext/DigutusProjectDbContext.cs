using DigutusProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DigutusProject.Repository.DbContext;

public class DigutusProjectDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DigutusProjectDbContext(DbContextOptions<DigutusProjectDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Time> Times { get; set; }
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
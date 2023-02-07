using DigutusProject.Core.UnitOfWorks;
using System;
using DigutusProject.Repository.DbContext;

namespace DigutusProject.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DigutusProjectDbContext _context;

    public UnitOfWork(DigutusProjectDbContext context)
    {
        _context = context;
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
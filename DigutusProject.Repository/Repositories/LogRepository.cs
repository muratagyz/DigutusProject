using DigutusProject.Core.Models;
using DigutusProject.Core.Repositories;
using DigutusProject.Repository.DbContext;

namespace DigutusProject.Repository.Repositories;

public class LogRepository : GenericRepository<Log>, ILogRepository
{
    public LogRepository(DigutusProjectDbContext context) : base(context)
    {
    }
}
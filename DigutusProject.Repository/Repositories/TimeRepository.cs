using DigutusProject.Core.Models;
using DigutusProject.Core.Repositories;
using DigutusProject.Repository.DbContext;

namespace DigutusProject.Repository.Repositories;

public class TimeRepository : GenericRepository<Time>, ITimeRepository
{
    public TimeRepository(DigutusProjectDbContext context) : base(context)
    {
    }
}
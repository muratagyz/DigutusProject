using DigutusProject.Core.Models;
using DigutusProject.Core.Repositories;
using DigutusProject.Repository.DbContext;

namespace DigutusProject.Repository.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DigutusProjectDbContext context) : base(context)
    {
    }
}
using DigutusProject.Core.Models;
using DigutusProject.Core.Repositories;
using DigutusProject.Core.UnitOfWorks;

namespace DigutusProject.Service.Services;

public class UserService : GenericService<User>, Core.Services.IUserService
{
    public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }
}
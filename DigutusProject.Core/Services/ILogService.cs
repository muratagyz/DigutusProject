using DigutusProject.Core.Models;

namespace DigutusProject.Core.Services;

public interface ILogService : IGenericService<Log>
{
    Task SignedInAndNotVerified(string email);
    Task SignedInAndVerified(string email);
}
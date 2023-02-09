using DigutusProject.Core.Models;

namespace DigutusProject.Core.Services;

public interface ITimeService : IGenericService<Time>
{
    Task Calculation(DateTime startTime, DateTime endTime);
}
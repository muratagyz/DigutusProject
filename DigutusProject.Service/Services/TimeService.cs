using DigutusProject.Core.Models;
using DigutusProject.Core.Repositories;
using DigutusProject.Core.Services;
using DigutusProject.Core.UnitOfWorks;

namespace DigutusProject.Service.Services;

public class TimeService : GenericService<Time>, ITimeService
{
    private readonly ITimeRepository _timeRepository;
    private readonly IUnitOfWork _unitOfWork;
    public TimeService(IGenericRepository<Time> repository, IUnitOfWork unitOfWork, ITimeRepository timeRepository) : base(repository, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _timeRepository = timeRepository;
    }

    public async Task Calculation(DateTime startTime, DateTime endTime)
    {
        var time = new Time()
        {
            Id = Guid.NewGuid(),
            StartTime = startTime,
            EndTime = endTime,
            CreateDate = DateTime.Now
        };
        await _timeRepository.AddAsync(time);
        await _unitOfWork.CommitAsync();
    }
}
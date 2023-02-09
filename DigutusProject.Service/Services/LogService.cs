using DigutusProject.Core.Models;
using DigutusProject.Core.Repositories;
using DigutusProject.Core.Services;
using DigutusProject.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace DigutusProject.Service.Services;

public class LogService : GenericService<Log>, ILogService
{
    private ILogRepository _logRepository;
    private IUnitOfWork _unitOfWork;
    public LogService(IGenericRepository<Log> repository, IUnitOfWork unitOfWork, ILogRepository logRepository) : base(repository, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logRepository = logRepository;
    }

    public async Task AddLog(string email)
    {
        var log = new Log()
        {
            Id = Guid.NewGuid(),
            CreateDate = DateTime.Now,
            Email = email,
            IsLogin = false
        };

        await _logRepository.AddAsync(log);
        await _unitOfWork.CommitAsync();
    }

    public async Task SignedInAndNotVerified(string email)
    {
        var getLog = await _logRepository.Where(x => x.Email == email).FirstOrDefaultAsync();

        if (getLog == null)
        {
            await AddLog(email);
        }
        else
        {
            var time = DateTime.Now - getLog.CreateDate;

            if (time.Days >= 1)
                await AddLog(email);
            else
                await SignedInAndVerified(email);
        }
    }

    public async Task SignedInAndVerified(string email)
    {
        var getLogUpdate = await _logRepository.Where(x => x.Email == email).FirstOrDefaultAsync();

        getLogUpdate.IsLogin = true;
        getLogUpdate.CreateDate = DateTime.Now;

        _logRepository.Update(getLogUpdate);
        await _unitOfWork.CommitAsync();
    }
}
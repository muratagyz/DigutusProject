using DigutusProject.Core.Enums;
using DigutusProject.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigutusProject.Web.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogService _logService;
        private readonly ITimeService _timeService;

        public HomeController(ILogService logService, ITimeService timeService)
        {
            _logService = logService;
            _timeService = timeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogInformation()
        {
            var tenDaysAgo = DateTime.Now.AddDays(-1);
            var Logs = _logService.Where(x => x.IsLogin == false && x.CreateDate >= tenDaysAgo).ToList();

            return View(Logs);
        }

        public async Task<IActionResult> TimeInformation()
        {
            var tenDaysAgo = DateTime.Now.AddDays(-1);
            var Times = _timeService.Where(x => x.CreateDate >= tenDaysAgo).ToList();

            return View(Times);
        }
    }
}

using DigutusProject.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigutusProject.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogService _logService;
        private readonly ITimeService _timeService;

        public HomeController(ILogService logService, ITimeService timeService)
        {
            _logService = logService;
            _timeService = timeService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LogInformation()
        {
            var tenDaysAgo = DateTime.Now.AddDays(-1);
            var Logs = _logService.Where(x => x.IsLogin == false && x.CreateDate >= tenDaysAgo).ToList();

            return View(Logs);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TimeInformation()
        {
            var tenDaysAgo = DateTime.Now.AddDays(-1);
            var Times = _timeService.Where(x => x.CreateDate >= tenDaysAgo).ToList();

            return View(Times);
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}

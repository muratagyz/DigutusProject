using DigutusProject.Core.DTOs;
using DigutusProject.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigutusProject.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var result = await _authService.LoginAsync(userLoginDto);

            if (!result)
                return RedirectToAction("Login");


            return View();
        }
    }
}

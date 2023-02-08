using DigutusProject.Core.DTOs;
using DigutusProject.Core.Services;
using DigutusProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigutusProject.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public static string Code { get; set; }

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> Login(string? message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var result = await _authService.LoginAsync(userLoginDto);

            if (!result)
                return RedirectToAction("Login", new { message = "Kullanıcı adı veya şifre yanlış" });

            Code = await _authService.GetVerificationCode(userLoginDto.Email);

            return RedirectToAction("VerificationCode");
        }

        public async Task<IActionResult> VerificationCode(string? message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerificationCode(VerificationCodeViewModel verificationCodeViewModel)
        {
            if (verificationCodeViewModel.Code != null)
                if (Code == verificationCodeViewModel.Code)
                {
                    return RedirectToAction("Login", new { message = "Giriş başarılı" });
                }
            return RedirectToAction("VerificationCode", new { message = "Doğrulama kodunuz yanlış veya eksik" });
        }
    }
}

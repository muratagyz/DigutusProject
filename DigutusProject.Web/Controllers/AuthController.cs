using DigutusProject.Core.DTOs;
using DigutusProject.Core.Models;
using DigutusProject.Core.Services;
using DigutusProject.Mail.Utilities;
using DigutusProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigutusProject.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private ILogService _logService;
        private ITimeService _timeService;
        public static string Code { get; set; }
        public static string Email { get; set; }
        public static DateTime StartTime { get; set; }
        public static DateTime EndTime { get; set; }

        public AuthController(IAuthService authService, ILogService logService, ITimeService timeService)
        {
            _authService = authService;
            _logService = logService;
            _timeService = timeService;
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

            StartTime = DateTime.Now;
            await _logService.SignedInAndNotVerified(userLoginDto.Email);

            Code = await _authService.GetVerificationCode(userLoginDto.Email);
            VerificationCodeHelper.verificationCode = "";
            Email = userLoginDto.Email;

            return RedirectToAction("VerificationCode");
        }

        public async Task<IActionResult> Register(string? message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var result = await _authService.RegisterAsync(userRegisterDto);
            return RedirectToAction("Login", new { message = result });
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
                    var role = await _authService.GetUserRole(Email);
                    var token = await _authService.LoginSuccessAsync(Email);
                    HttpContext.Session.SetString("JWToken", token.Token.ToString());
                    HttpContext.Session.SetString("Role", role);
                    await _logService.SignedInAndVerified(Email);
                    EndTime = DateTime.Now;
                    await _timeService.Calculation(StartTime, EndTime);
                    return RedirectToAction("Index", "Home");
                }

            return RedirectToAction("VerificationCode", new { message = "Doğrulama kodunuz yanlış veya eksik" });
        }

        public async Task<IActionResult> ResetPassword(string? message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var result = await _authService.ResetPassword(resetPasswordViewModel.Email);

            if (result)
                return RedirectToAction("Login", new { message = "Yeni şifrenizi email adresinize gönderdik." });

            return RedirectToAction("ResetPassword", new { message = "Beklenmedik bir hata oluştu" });
        }
    }
}

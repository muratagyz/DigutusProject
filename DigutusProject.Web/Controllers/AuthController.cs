using DigutusProject.Core.DTOs;
using DigutusProject.Core.Services;
using DigutusProject.Mail.Utilities;
using DigutusProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigutusProject.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public static string Code { get; set; }
        public static string Email { get; set; }

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
                    var token = await _authService.LoginSuccessAsync(Email);
                    HttpContext.Session.SetString("JWToken", token.Token.ToString());

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

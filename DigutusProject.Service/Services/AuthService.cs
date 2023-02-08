using DigutusProject.Core.DTOs;
using DigutusProject.Core.Services;
using DigutusProject.Core.Utilities.Security.Hashing;
using DigutusProject.Mail;
using DigutusProject.Mail.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DigutusProject.Service.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IMailService mailService;

    public AuthService(IUserService userService, IMailService mailService)
    {
        _userService = userService;
        this.mailService = mailService;
    }

    public async Task<bool> LoginAsync(UserLoginDto userLoginDto)
    {
        var CheckEmail = await _userService.AnyAsync(x => x.Email == userLoginDto.Email);

        if (!CheckEmail)
            return CheckEmail;

        var user = await _userService.Where(x => x.Email == userLoginDto.Email).FirstOrDefaultAsync();

        var PasswordCheck = HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt);

        return PasswordCheck;
    }

    public Task<bool> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetVerificationCode(string email)
    {
        var code = VerificationCodeHelper.CreateVerificationCode();
        var mail = new MailRequest();
        mail.ToEmail = email;
        mail.Subject = "Doğrulama Kodu";
        mail.Body = code;
        await mailService.SendEmailAsync(mail);

        return code;
    }
}
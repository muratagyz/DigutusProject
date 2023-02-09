using DigutusProject.Core.DTOs;
using DigutusProject.Core.Enums;
using DigutusProject.Core.Models;
using DigutusProject.Core.Services;
using DigutusProject.Core.Utilities.Security.Hashing;
using DigutusProject.Core.Utilities.Security.Jwt;
using DigutusProject.Mail;
using DigutusProject.Mail.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DigutusProject.Service.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IMailService mailService;
    private readonly ITokenHelper _tokenHelper;

    public AuthService(IUserService userService, IMailService mailService, ITokenHelper tokenHelper)
    {
        _userService = userService;
        this.mailService = mailService;
        _tokenHelper = tokenHelper;
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

    public async Task<AccessToken> LoginSuccessAsync(string email)
    {
        var user = await _userService.Where(x => x.Email == email).FirstOrDefaultAsync();

        if(user == null)
            return null;

        var token = _tokenHelper.CreateToken(user);

        return token;
    }

    public async Task<string> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        var userCheck = await _userService.AnyAsync(x => x.Email == userRegisterDto.Email);
        if (userCheck)
            return "Böyle bir email adresi mevcuttur";

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);

        var user = new User()
        {
            Id = new Guid(),
            Email = userRegisterDto.Email,
            CreateDate = DateTime.Now,
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Role = Role.User,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await _userService.AddAsync(user);

        return "Başarılı bir şekilde kaydoldunuz";
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

    public async Task<bool> ResetPassword(string email)
    {
        var userCheck = await _userService.AnyAsync(x => x.Email == email);

        if(!userCheck)
            return false;

        var getUser = await _userService.Where(x => x.Email == email).FirstOrDefaultAsync();

        byte[] passwordHash, passwordSalt;
        var newPassword = ResetPasswordHelper.CreateNewPassword();
        HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

        getUser.PasswordHash = passwordHash;
        getUser.PasswordSalt = passwordSalt;
        await _userService.UpdateAsync(getUser);

        var mail = new MailRequest();
        mail.ToEmail = email;
        mail.Subject = "Parola Sıfırlama";
        mail.Body = newPassword;
        await mailService.SendEmailAsync(mail);

        return true;
    }

    public async Task<string> GetUserRole(string email)
    {
        var user = await _userService.Where(x => x.Email == email).FirstOrDefaultAsync();
        return user.Role.ToString();
    }
}
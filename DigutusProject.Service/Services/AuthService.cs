using DigutusProject.Core.DTOs;
using DigutusProject.Core.Services;
using DigutusProject.Core.Utilities.Security.Hashing;
using Microsoft.EntityFrameworkCore;

namespace DigutusProject.Service.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
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
}
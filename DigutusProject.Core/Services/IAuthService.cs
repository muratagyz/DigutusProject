using DigutusProject.Core.DTOs;
using DigutusProject.Core.Utilities.Security.Jwt;

namespace DigutusProject.Core.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(UserLoginDto userLoginDto);
    Task<AccessToken> LoginSuccessAsync(string email);
    Task<string> RegisterAsync(UserRegisterDto userRegisterDto);
    Task<string> GetVerificationCode(string email);
    Task<bool> ResetPassword(string email);
    Task<string> GetUserRole(string email);
}
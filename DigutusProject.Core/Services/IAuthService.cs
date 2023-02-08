using DigutusProject.Core.DTOs;

namespace DigutusProject.Core.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(UserLoginDto userLoginDto);
    Task<bool> RegisterAsync(UserRegisterDto userRegisterDto);
}
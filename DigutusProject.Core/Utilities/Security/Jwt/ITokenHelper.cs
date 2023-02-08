using DigutusProject.Core.Models;

namespace DigutusProject.Core.Utilities.Security.Jwt;

public interface ITokenHelper
{
    AccessToken CreateToken(User user);
}
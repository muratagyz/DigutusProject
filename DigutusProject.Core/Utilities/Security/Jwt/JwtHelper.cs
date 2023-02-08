using System.IdentityModel.Tokens.Jwt;
using DigutusProject.Core.Models;
using DigutusProject.Core.Utilities.Security.Encyption;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using DigutusProject.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace DigutusProject.Core.Utilities.Security.Jwt;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };

    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials)
    {
        var Jwt = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClains(user),
            signingCredentials: signingCredentials
        );
        return Jwt;
    }

    private IEnumerable<Claim> SetClains(User user)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddName($"{user.FirstName} {user.LastName}");
        claims.AddRole(user.Role.ToString());
        return claims;
    }
}
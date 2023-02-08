﻿using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DigutusProject.Core.Extensions;

public static class ClaimExtensions
{
    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new Claim(type: JwtRegisteredClaimNames.Email, value: email));
    }
    public static void AddName(this ICollection<Claim> claims, string name)
    {
        claims.Add(new Claim(type: ClaimTypes.Name, value: name));
    }
    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        claims.Add(new Claim(type: ClaimTypes.NameIdentifier, value: nameIdentifier));
    }
    public static void AddRole(this ICollection<Claim> claims, string role)
    {
        claims.Add(new Claim(type: ClaimTypes.Role, value: role));
    }
}
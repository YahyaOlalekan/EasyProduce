using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Authentication;
using Application.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace Persistence.Authentication;

public class TokenService : ITokenService1
{
    private const int ExpirationMinutes = 300;
    public string CreateToken(JwtUserTokenClaims model)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(CreateClaims(model), CreateSigningCredentials(), expiration);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration) => new JwtSecurityToken("EasyAuthIssuer", "https://localhost:5001/", claims, expires: expiration, signingCredentials: credentials);

    private List<Claim> CreateClaims(JwtUserTokenClaims model)
    {
        try
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Role, model.RoleName),
                    // new Claim("RoleName", model.RoleName),
                    // new Claim("UserId", model.UserId.ToString()),
                    
                };
            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private SigningCredentials CreateSigningCredentials()
    {
        var signingCredentials = new SigningCredentials(
          new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xkeydas-3323bc7evvwq6679fa13b125c9322aa7cd289955bcfeeb8e5fd1a284542f82-b46RYkdlnXuIIUQuu9")), SecurityAlgorithms.HmacSha256);
        return signingCredentials;
    }
}


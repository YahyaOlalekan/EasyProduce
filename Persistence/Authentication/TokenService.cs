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
    public string CreateToken(UserDto model)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(CreateClaims(model), CreateSigningCredentials(), expiration);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration) => new JwtSecurityToken("EasyAuthIssuer", "https://localhost:5001/", claims, expires: expiration, signingCredentials: credentials);

    private List<Claim> CreateClaims(UserDto model)
    {
        try
        {
            var claims = new List<Claim>
                {
                    //new Claim(ClaimTypes.Name, model.FirstName),
                    new Claim("testingkey", "TokenForTheApiWithAuth"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                    new Claim(ClaimTypes.Email, model.Email),
                    //new Claim(ClaimTypes.GivenName, model.FirstName),
                    new Claim(ClaimTypes.Role, model.RoleId.ToString()),
                    //new Claim(ClaimTypes.Actor, model.UserId),
                    //new Claim(ClaimTypes.UserData, model.ProfilePicture)
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


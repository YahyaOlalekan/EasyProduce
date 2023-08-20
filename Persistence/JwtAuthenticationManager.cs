﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Application.Abstractions;
using Application.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;

namespace Persistence
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        public string GenerateToken(string key, string issuer, UserDto user)
        {
            // _ = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddHours(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }

        // public bool IsTokenValid(string key, string issuer, string token)
        // {
        //     var mySecret = Encoding.UTF8.GetBytes(key);
        //     var mySecurityKey = new SymmetricSecurityKey(mySecret);

        //     var tokenHandler = new JwtSecurityTokenHandler();

        //     try
        //     {
        //         tokenHandler.ValidateToken(token, new TokenValidationParameters
        //         {
        //             ValidateIssuerSigningKey = true,
        //             ValidateIssuer = true,
        //             ValidateAudience = true,
        //             ValidateLifetime = true,
        //             ValidIssuer = issuer,
        //             ValidAudience = issuer,
        //             IssuerSigningKey = mySecurityKey,
        //         }, out SecurityToken validatedToken);
        //     }
        //     catch
        //     {
        //         return false;
        //     }
        //     return true;
        // }
        // public static int GetLoginId(string token)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var jwtToken = tokenHandler.ReadJwtToken(token);

        //     // Retrieve the ID from the claims
        //     var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //     if (idClaim != null)
        //     {
        //         string userId = idClaim.Value;

        //         // Use the userId as needed
        //         return int.Parse(userId);
        //     }
        //     else
        //     {
        //         // Handle the case when the ID claim is not present in the token
        //         return 0;
        //     }
        // }
    }
}

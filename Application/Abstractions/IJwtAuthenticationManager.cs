using Application.Dtos;

namespace Application.Abstractions
{
    public interface IJwtAuthenticationManager
    {
        string GenerateToken(string key, string issuer, UserDto user);
        // bool IsTokenValid(string key, string issuer, string token);
    }
}
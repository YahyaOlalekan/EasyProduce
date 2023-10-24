using Application.Dtos;

namespace Application.Authentication;

public interface ITokenService1
{
    string CreateToken(UserDto model);
}

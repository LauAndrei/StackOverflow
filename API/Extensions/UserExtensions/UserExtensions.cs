using API.Dtos;
using Core.Entities;

namespace API.Extensions.UserExtensions;

public static class UserExtensions
{
    public static User ToUser(this RegisterDto registerDto)
    {
        return new()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            PasswordHash = registerDto.Password
        };
    }

    public static UserDto ToUserDto(this User user, string token)
    {
        return new()
        {
            Email = user.Email,
            UserName = user.UserName,
            Score = user.Score,
            Token = token
        };
    }
}
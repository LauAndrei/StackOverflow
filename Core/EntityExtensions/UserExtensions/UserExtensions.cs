using Core.Dtos.UserDtos;
using Core.Entities;

namespace Core.EntityExtensions.UserExtensions;

public static class UserExtensions
{
    public static User ToUser(this RegisterDto registerDto)
    {
        return new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            PasswordHash = registerDto.Password
        };
    }

    public static LoggedInUserDto ToLoggedInUserDto(this User user, string token)
    {
        return new LoggedInUserDto
        {
            Email = user.Email,
            UserName = user.UserName,
            Score = user.Score,
            Token = token
        };
    }

    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Score = user.Score
        };
    }
}
using Core.Entities;

namespace Core.Interfaces.ServiceInterfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}
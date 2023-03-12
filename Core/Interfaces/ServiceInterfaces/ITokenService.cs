using Core.Entities;

namespace Core.Interfaces.ServiceInterfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
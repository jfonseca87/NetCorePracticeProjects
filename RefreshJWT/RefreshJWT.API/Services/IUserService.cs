using RefreshJWT.API.Models;

namespace RefreshJWT.API.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<AuthenticateReponse> LoginAsync(AuthenticateRequest authenticate, string ipAddress);
    Task<AuthenticateReponse> RefreshTokenAsync(string token, string ip);
}
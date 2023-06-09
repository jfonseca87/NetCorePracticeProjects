using RefreshJWT.API.Models;

namespace RefreshJWT.API.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetLoginUserAsync(AuthenticateRequest authenticate);
    Task RemoveOldRefreshTokens(User user);
    Task<User> GetUserByRefreshTokenAsync(string token);
    Task UpdateUserAsync(User user);
}
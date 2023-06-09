using Microsoft.EntityFrameworkCore;
using RefreshJWT.API.Models;

namespace RefreshJWT.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly RefreshTokenContext _context;

    public UserRepository(RefreshTokenContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsersAsync() => 
        await _context.User.ToListAsync();
    
    public async Task<User> GetUserByIdAsync(int id) => 
        await _context.User.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<User> GetLoginUserAsync(AuthenticateRequest authenticate) => 
        await _context.User.FirstOrDefaultAsync(x => x.Username == authenticate.Username);

    public async Task RemoveOldRefreshTokens(User user)
    {
        user.RefreshTokens.RemoveAll(x => !x.IsActive && x.Created.AddDays(2) <= DateTime.UtcNow);
        _context.Update(user);

        await _context.SaveChangesAsync();
    }
    
    public async Task<User> GetUserByRefreshTokenAsync(string token)
    {
        return await _context.User.FirstOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == token));
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}
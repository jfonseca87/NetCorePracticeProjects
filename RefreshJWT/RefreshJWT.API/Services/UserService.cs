using RefreshJWT.API.Models;
using RefreshJWT.API.Repositories;
using RefreshJWT.API.Utils;

namespace RefreshJWT.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwt _jwt;

    public UserService(IUserRepository userRepository, IJwt jwt)
    {
        _userRepository = userRepository;
        _jwt = jwt;
    }

    public async Task<IEnumerable<User>> GetUsersAsync() =>
        await _userRepository.GetUsersAsync();

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<AuthenticateReponse> LoginAsync(AuthenticateRequest authenticate, string ipAddress)
    {
        var user = await _userRepository.GetLoginUserAsync(authenticate);
        if (user is null)
        {
            return new AuthenticateReponse { Id = 0 };
        }

        bool isValidPassword = BCrypt.Net.BCrypt.Verify(authenticate.Password, user.Password);
        if (!isValidPassword)
        {
            return new AuthenticateReponse { Id = 0 };
        }

        (string token, long expires) = _jwt.GenerateJWT(user.Id, user.Username, user.Email);
        RefreshToken refreshToken = _jwt.GenerateRefreshToken(ipAddress);
        user.RefreshTokens.Add(refreshToken);

        await _userRepository.RemoveOldRefreshTokens(user);

        return new AuthenticateReponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            JwtToken = token,
            Expires = expires,
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<AuthenticateReponse> RefreshTokenAsync(string token, string ip)
    {
        User user = await _userRepository.GetUserByRefreshTokenAsync(token);
        RefreshToken refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        if (refreshToken.IsRevoked)
        {
            RevokeDescendantRefreshToken(refreshToken, user, ip, $"Attempted reuse of revoked ancestor token: {refreshToken.Token}");
            await _userRepository.UpdateUserAsync(user);
        }

        if (!refreshToken.IsActive)
        {
            throw new Exception("Invalid Token");
        }

        RefreshToken newRefreshToken = RenewRefreshToken(refreshToken, ip);
        user.RefreshTokens.Add(newRefreshToken);

        await _userRepository.RemoveOldRefreshTokens(user);

        // Generate JWT Token
        (string newJwtToken, long expires) = _jwt.GenerateJWT(user.Id, user.Username, user.Email);

        return new AuthenticateReponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            JwtToken = newJwtToken,
            Expires = expires,
            RefreshToken = newRefreshToken.Token
        };
    }

    private void RevokeDescendantRefreshToken(RefreshToken refreshToken, User user, string ip, string reason)
    {
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        {
            var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
            if (childToken.IsActive)
            {
                RevokeRefreshToken(childToken, ip, reason);
            }
            else
            {
                RevokeDescendantRefreshToken(childToken, user, ip, reason);
            }
        }
    }

    private RefreshToken RenewRefreshToken(RefreshToken refreshToken, string ip)
    {
        var newRefreshToken = _jwt.GenerateRefreshToken(ip);
        RevokeRefreshToken(refreshToken, ip, "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    private void RevokeRefreshToken(RefreshToken refreshToken, string ip, string reason, string replaceByToken = null)
    {
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ip;
        refreshToken.ReasonRevoked = reason;
        refreshToken.ReplacedByToken = replaceByToken;
    }
}
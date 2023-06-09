using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RefreshJWT.API.Models;
using RefreshJWT.API.Repositories;

namespace RefreshJWT.API.Utils;

public class Jwt : IJwt
{
    private readonly IConfiguration _configuration;
    private readonly RefreshTokenContext _context;

    public Jwt(IConfiguration configuration, RefreshTokenContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public (string token, long expires) GenerateJWT(int userId, string userName, string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTConfiguration:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        int.TryParse(_configuration["JWTConfiguration:ExpireTime"], out int expireMinutes);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        DateTime dateExpire = DateTime.Now.AddMinutes(expireMinutes);
        var securityToken = new JwtSecurityToken(
            issuer: _configuration["JWTConfiguration:Issuer"],
            audience: _configuration["JWTConfiguration:Audience"],
            claims: claims,
            expires: dateExpire,
            signingCredentials: credentials);

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        long expires = dateExpire.Ticks;

        return (token, expires);
    }

    public RefreshToken GenerateRefreshToken(string ipAddress)
    {
        return new RefreshToken
        {
            Token = GetUniqueToken(),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }

    private string GetUniqueToken()
    {
        string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        bool isUniqueToken = !_context.User.Any(x => x.RefreshTokens.Any(x => x.Token == token));

        if (!isUniqueToken)
        {
            return GetUniqueToken();
        }

        return token;
    }
}
using RefreshJWT.API.Models;

namespace RefreshJWT.API.Utils;

public interface IJwt
{
    (string token, long expires) GenerateJWT(int userId, string userName, string email);
    RefreshToken GenerateRefreshToken(string ipAddress);
}
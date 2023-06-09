using System.Text.Json.Serialization;

namespace RefreshJWT.API.Models;

public class AuthenticateReponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string JwtToken { get; set; }
    public long Expires { get; set; }

    [JsonIgnore]
    public string RefreshToken { get; set; }
}
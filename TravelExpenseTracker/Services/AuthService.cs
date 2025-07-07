using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.Services
{
    public class AuthService
    {
        private const string TokenKey = "token";

        public string? Token { get; private set; }

        public LoggedInUser User { get; set; } = new(0, "", "");

        public void SetToken(string token)
        {
            Token = token;
            User = GetUserFromToken(token);
            Preferences.Default.Set(TokenKey, token);
        }

        public void RemoveToken()
        {
            Token = null;
            User = new(0, "", "");
            Preferences.Default.Remove(TokenKey);
        }

        public void Initialize()
        {
            if(Preferences.Default.ContainsKey(TokenKey))
            {
                var token = Preferences.Default.Get<string?>(TokenKey, null);
                if (string.IsNullOrWhiteSpace(token) || IsTokenExpired(token!))
                {
                    RemoveToken();
                    return;
                }
                SetToken(token!);
            }
        }

        private JwtSecurityToken ParseToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }

        private LoggedInUser GetUserFromToken(string token)
        {
            var jwtToken = ParseToken(token);
            var userId = Convert.ToInt32(jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            var name = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return new LoggedInUser(userId, name!, email!);
        }

        public bool IsTokenExpired(string token)
        {
            var jwtToken = ParseToken(token);
            return jwtToken.ValidTo <= DateTime.UtcNow;
        }
    }
}

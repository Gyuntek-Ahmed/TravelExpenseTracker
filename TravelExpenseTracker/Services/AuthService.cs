namespace TravelExpenseTracker.Services
{
    public class AuthService
    {
        private const string TokenKey = "token";

        public string? Token { get; private set; }

        public void SetToken(string token)
         => Preferences.Default.Set(TokenKey, token);
        
        public void RemoveToken()
         => Preferences.Default.Remove(TokenKey);
    }
}

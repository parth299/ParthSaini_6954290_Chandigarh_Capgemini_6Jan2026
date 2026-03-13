using Microsoft.AspNetCore.Http;

namespace MyApp.Services
{
    public class AuthService : IAuthService
    {
        public bool IsAuthenticated(HttpContext context)
        {
            return context.Session.GetString("User") != null;
        }

        public bool ValidateUser(string username, string password)
        {
            // Example validation (replace with DB later)
            if (username == "admin" && password == "1234")
            {
                return true;
            }

            return false;
        }
    }
}
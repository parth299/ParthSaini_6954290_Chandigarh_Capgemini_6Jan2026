namespace MyApp.Services
{
    public interface IAuthService
    {
        bool IsAuthenticated(HttpContext context);
        bool ValidateUser(string username, string password);
    }
}
using Microsoft.AspNetCore.Http;
using MyApp.Services;
using System.Threading.Tasks;

namespace MyApp.Middleware
{
    public class AdminAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            if (context.Request.Path.StartsWithSegments("/Admin"))
            {
                if (!authService.IsAuthenticated(context))
                {
                    context.Response.Redirect("/Students/Login");
                    return;
                }
            }

            await _next(context);
        }
    }
}
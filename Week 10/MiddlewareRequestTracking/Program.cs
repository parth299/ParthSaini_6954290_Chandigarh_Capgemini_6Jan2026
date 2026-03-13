using StudentPortal.Middleware;
using StudentPortal.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IRequestLogService, RequestLogService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

// Add Custom Middleware
app.UseMiddleware<RequestTrackingMiddleware>();

// Conventional Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}"
);

app.Run();
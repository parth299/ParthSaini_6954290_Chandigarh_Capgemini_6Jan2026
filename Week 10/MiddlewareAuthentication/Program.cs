using MyApp.Middleware;
using MyApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

// Custom Middleware
app.UseMiddleware<AdminAuthMiddleware>();

app.UseEndpoints(endpoints =>
{
    // Protected Admin Routes
    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "Admin/{action=Dashboard}/{id?}",
        defaults: new { controller = "Admin" });

    // Public Students Routes
    endpoints.MapControllerRoute(
        name: "students",
        pattern: "Students/{action=Index}/{id?}",
        defaults: new { controller = "Students" });

    // Default Route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Students}/{action=Index}/{id?}");
});

app.Run();
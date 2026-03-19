var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

// ✅ Add Session service
builder.Services.AddSession();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

// ✅ Enable Session
app.UseSession();

app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
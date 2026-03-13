using LibraryManagement.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register Repository with DI
builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Details}/{id?}"
);

app.Run();
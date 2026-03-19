using ProductManagement.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add MVC with global exception filter
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CustomExceptionFilter>(); // Global exception filter
});

// Register logging filter
builder.Services.AddScoped<LogActionFilter>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
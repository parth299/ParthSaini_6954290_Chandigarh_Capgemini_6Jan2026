var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// 🔴 REQUIRED — Add HttpClient to call API
builder.Services.AddHttpClient("BookAPI", client =>
{
    client.BaseAddress =
        new Uri("https://bookstore-api-parth-gmbzc4b0fbb4gjbb.centralindia-01.azurewebsites.net/");
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


// 🔴 Route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
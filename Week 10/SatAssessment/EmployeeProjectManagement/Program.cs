using EmployeeProjectManagement.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeProjectManagement.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs1")));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.Run();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Departments.Any())
    {
        context.Departments.AddRange(
            new Department { Name = "HR" },
            new Department { Name = "IT" },
            new Department { Name = "Finance" }
        );

        context.SaveChanges();
    }
}
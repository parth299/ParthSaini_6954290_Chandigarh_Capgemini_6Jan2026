using Microsoft.EntityFrameworkCore;
using BookStore.API.Data;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Add Services
// ----------------------------

// Add Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVC",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// ----------------------------
// Build App
// ----------------------------

var app = builder.Build();


// ----------------------------
// Configure Middleware Pipeline
// ----------------------------

// Swagger in Development
app.UseSwagger();
app.UseSwaggerUI();


// HTTPS Enforcement
app.UseHttpsRedirection();

// CORS
app.UseCors("AllowMVC");

// Authorization (future-ready)
app.UseAuthorization();

// Map Controllers
app.MapControllers();


// ----------------------------
// Run Application
// ----------------------------

app.Run();
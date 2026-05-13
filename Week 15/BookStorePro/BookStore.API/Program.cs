using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BookStore.Infrastructure.Repositories;
using BookStore.Application.Interfaces;
using BookStore.Application.MappingProfiles;
using Microsoft.AspNetCore.Mvc;
using BookStore.Application.Services;
using BookStore.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion =
        new ApiVersion(1, 0);

    options.AssumeDefaultVersionWhenUnspecified = true;

    options.ReportApiVersions = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration
            .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<
    IBookRepository,
    BookRepository>();

builder.Services.AddScoped<
    IOrderRepository,
    OrderRepository>();

builder.Services.AddScoped<
    IBookService,
    BookService>();

builder.Services.AddScoped<
    IOrderService,
    OrderService>();

builder.Services.AddScoped<
    ITokenService,
    TokenService>();

builder.Services.AddScoped<
    IPaymentService,
    PaymentService>();

builder.Services.AddScoped<
    IReportService,
    ReportService>();

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddAutoMapper(
    typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();


app.Run();


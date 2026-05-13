using System.Text;
using BookStore.Application.Interfaces;
using BookStore.Application.MappingProfiles;
using BookStore.Application.Services;
using BookStore.Application.Validators;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<BookStoreDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BookStoreConnection")));

        // AutoMapper
        services.AddAutoMapper(new[] { typeof(MappingProfile) });

        // FluentValidation
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();

        // JWT Configuration
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? throw new ArgumentNullException("JwtSettings:SecretKey");
        var issuer = jwtSettings["Issuer"] ?? "BookStoreAPI";
        var audience = jwtSettings["Audience"] ?? "BookStoreClients";
        var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"] ?? "60");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        // Repositories
        services.AddScoped<IGenericRepository<BookStore.Domain.Entities.Role>, GenericRepository<BookStore.Domain.Entities.Role>>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        // Services
        services.AddScoped<ITokenService>(provider => new TokenService(secretKey, issuer, audience, expirationMinutes));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IWishlistService, WishlistService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IEmailService, EmailService>();

        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseApplicationMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<Middleware.GlobalExceptionMiddleware>();
        return app;
    }
}

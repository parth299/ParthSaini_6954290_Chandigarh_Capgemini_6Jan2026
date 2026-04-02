using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OnlineLearningPlatform.API.Helpers;
using OnlineLearningPlatform.API.Profiles;
using Microsoft.AspNetCore.Mvc;  
using System.Linq;              

var builder = WebApplication.CreateBuilder(args);

// ===============================
// DATABASE CONFIGURATION
// ===============================

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// ===============================
// CONTROLLERS
// ===============================

builder.Services.AddMemoryCache();
builder.Services.AddControllers();


// ===============================
// SWAGGER
// ===============================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSwaggerGen(options =>
{
    // Enable JWT in Swagger

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your token"
    });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference =
                        new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type =
                                Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                new string[] {}
            }
        });
});


// ===============================
// JWT SERVICE
// ===============================

builder.Services.AddScoped<JwtService>();


// ===============================
// AUTHENTICATION
// ===============================

builder.Services
.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration["Jwt:Issuer"],

            ValidAudience =
                builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]))
        };
});


// ===============================
// AUTHORIZATION
// ===============================

builder.Services.AddAuthorization();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory =
        context =>
        {
            var errors =
                context.ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

            return new BadRequestObjectResult(
                new
                {
                    error =
                        string.Join(
                            ", ",
                            errors)
                });
        };
});

// ===============================
// BUILD APP
// ===============================

var app = builder.Build();


// ===============================
// MIDDLEWARE PIPELINE
// ===============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// 🔐 VERY IMPORTANT ORDER

app.UseAuthentication();

app.UseAuthorization();


// ===============================
// MAP CONTROLLERS
// ===============================

app.MapControllers();

app.Run();
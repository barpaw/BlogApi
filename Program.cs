using System.Text;
using BlogApi.Application.Middleware.Asp;
using BlogApi.Application.Middleware.Mediatr;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.Auth;
using BlogApi.Infrastructure.Data;
using BlogApi.Infrastructure.Services;
using BlogApi.Shared.Extensions.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    options.OperationFilter<SecurityRequirementsOperationFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Blog API",
        Description = "Blog API (ASP.NET Core Web API)"
    });

    options.AddServer(new OpenApiServer { Url = "/api" });
    options.SupportNonNullableReferenceTypes();

    options.DocInclusionPredicate((_, _) => true);

    options.EnableAnnotations();
});

// builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
//
// builder.Services.AddAuthorizationBuilder();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); 
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var Configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{environment}.json")
    .AddEnvironmentVariables()
    .Build();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionString);

    if (environment == "Development")
    {
        options.EnableSensitiveDataLogging();
    }

    options.UseLazyLoadingProxies();
});

// builder.Services.AddIdentityCore<User>(options =>
//     {
//         options.User.RequireUniqueEmail = true;
//         options.Lockout.MaxFailedAccessAttempts = 7;
//     }).AddEntityFrameworkStores<AppDbContext>()
//     .AddApiEndpoints();

// For Identity  
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

// Adding Jwt Bearer  
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
            ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
        };
    });


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.RegisterRepositories();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITokenService, TokenService>();

// Fluent Validator
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    
var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UsePathBase("/api");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using System.Reflection;
using System.Security.Principal;
using BlogApi.Core.Entities;
using BlogApi.Infrastructure.Data;
using BlogApi.Shared.Extensions.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

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

builder.Services.AddIdentityCore<User>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Lockout.MaxFailedAccessAttempts = 7;
    }).AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.RegisterRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapIdentityApi<User>();


app.MapControllers();

app.UsePathBase("/api");

app.Run();
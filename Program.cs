using StoreCRM.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StoreCRM.Helpers;
using StoreCRM.Interfaces;
using StoreCRM.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using StoreCRM;
using Microsoft.OpenApi.Models;
using StoreCRM.DTOs;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Store CRM API",
        Version = "v1",
        Description = ""
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    options.CustomSchemaIds((Type currentClass) =>
    {
        var returnedValue = currentClass.Name;

        return returnedValue.EndsWith("DTO")
            ? returnedValue.Replace("DTO", string.Empty)
            : returnedValue;
    });
});

builder.Services.AddDbContext<StoreCrmDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var mapperConfig = new MapperConfiguration(options =>
{
    options.AddProfile(new MapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<UserResolver>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProductsService, ProductsService>();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,

        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,

        ValidateLifetime = true,

        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddCors(o => o.AddPolicy("GodPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("GodPolicy");

#endregion

app.Run();


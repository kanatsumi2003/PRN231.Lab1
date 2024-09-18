﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Petalaka.Account.Contract.Repository.CustomSettings;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.API;

public static class ConfigureService
{
    public static void AddConfigureServiceAPI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtSettings();
        services.AddAuthenticationJwt(configuration);
        services.ConfigRoute();
        services.AddCors();
        services.AddSwagger();
    }
    
    public static void ConfigRoute(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
    }
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Order API", Version = "v1" });
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: \"Bearer yourTokenHere\"",
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        });
        
    }
    public static void AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }
    public static void AddJwtSettings(this IServiceCollection services)
    {
        IConfiguration configuration = ReadConfiguration.ReadAppSettings();
        services.AddSingleton<JwtSettings>(options =>
        {
            JwtSettings jwtSettings = new JwtSettings
            {
                Key = configuration.GetSection("JwtSettings:Key").Value,
                Issuer = configuration.GetSection("JwtSettings:Issuer").Value,
                Audience = configuration.GetSection("JwtSettings:Audience").Value,
                AccessTokenExpirationMinutes =
                    Convert.ToInt32(configuration.GetSection("JwtSettings:AccessTokenExpiresInMinutes").Value),
                RefreshTokenExpirationDays =
                    Convert.ToInt32(configuration.GetSection("JwtSettings:RefreshTokenExpiresInMinutes").Value)
            };
            jwtSettings.IsValid();
            return jwtSettings;
        });
    }

    public static void AddAuthenticationJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = ReadConfiguration.ReadAppSettings().GetSection("JwtSettings");
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                ValidAudience = jwtSettings.GetSection("Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("Key").Value)),
                ClockSkew = TimeSpan.Zero // No tolerance for token expiration
            };
        });
    }
}
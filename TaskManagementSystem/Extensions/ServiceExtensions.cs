using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManagementSystem.Application;
using TaskManagementSystem.Application.Common.Behaviours;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Jwt;
using TaskManagementSystem.Infrastructure;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Repository;

namespace TaskManagementSystem.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registers application-level services.
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            // add validators
            services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
            // Register MediatR from the Application layer
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));
            });

            return services;
        }

        /// <summary>
        /// Configures Identity with ApplicationUser.
        /// </summary>
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        /// <summary>
        /// Configures JWT authentication.
        /// </summary>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = new JwtOptions();
            configuration.GetSection("Jwt").Bind(jwtOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = jwtOptions.Issuer, // Should match "Issuer" in appsettings.json
                         ValidAudience = jwtOptions.Audience, // Should match "Audience" in appsettings.json
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                     };

                     options.Events = new JwtBearerEvents
                     {
                         OnAuthenticationFailed = context =>
                         {
                             Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                             return Task.CompletedTask;
                         }
                     };
                 });


            return services;
        }

        /// <summary>
        /// Configures Swagger with JWT Bearer authentication.
        /// </summary>
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Task Management API",
                    Version = "v1",
                    Description = "Task Management API with JWT Authentication",
                    Contact = new OpenApiContact
                    {
                        Name = "Your Name",
                        Email = "your@email.com"
                    }
                });

                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
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

            return services;
        }
    }
}

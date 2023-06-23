using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.Security;
using BankRateAggregator.Infrastructure.Common.Identity;
using BankRateAggregator.Infrastructure.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankRateAggregator.Infrastructure.ServiceRegistration;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), o =>
            {
                o.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(15),
                    errorCodesToAdd: null);
            }));

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IJwtTokenValidator, JwtTokenValidator>();

        services.RegisterAuthentication(configuration);
        services.AddAuthorization(options =>
        {
            options.AddPolicyOptions();
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;

    }
    private static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetRequiredSection("Identity:Issuer").Value ?? throw new InvalidOperationException("Identity:Issuer section was not found"),
                    ValidAudience = configuration.GetRequiredSection("Identity:Audience").Value ?? throw new InvalidOperationException("Identity:Audience section was not found"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetRequiredSection("Identity:SigninCredential")?.Value ?? throw new InvalidOperationException("Identity:SigninCredential section was not found")))
                };
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new JwtTokenValidator(configuration));
            });
    }

    private static void AddPolicyOptions(this AuthorizationOptions options)
    {
        options.AddPolicy("Admin", p => p.RequireAuthenticatedUser()
               .RequireRole(RoleNames.Admin));

        options.AddPolicy("User", p => p.RequireAuthenticatedUser()
               .RequireRole(RoleNames.User));
    }
}

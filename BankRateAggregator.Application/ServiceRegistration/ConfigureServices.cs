using BankRateAggregator.Application.Common.Behaviours;
using BankRateAggregator.Application.Services.Banks;
using BankRateAggregator.Application.Services.Banks.Interfaces;
using BankRateAggregator.Application.Services.Currency;
using BankRateAggregator.Application.Services.Currency.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Claims;

namespace BankRateAggregator.Application.ServiceRegistration;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        });

        services.AddSingleton<ClaimsPrincipal>();
        services.AddScoped<ICurrencyParserService, CurrencyParserService>();
        services.AddScoped<IBankService, BankService>();

        return services;
    }
}

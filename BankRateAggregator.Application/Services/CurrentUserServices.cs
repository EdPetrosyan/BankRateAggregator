using BankRateAggregator.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BankRateAggregator.Application.Services;

public class CurrentUserServices : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserServices(IHttpContextAccessor contextAccessor)
    {

        _contextAccessor = contextAccessor;
    }

    public string? UserId => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}

using BankRateAggregator.WebAPI.Filters;
using BankRateAggregator.WebAPI.HttpResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankRateAggregator.WebAPI.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ILogger<ApiControllerBase> _logger;
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    protected Guid UserId => new(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    protected ApiControllerBase(ILogger<ApiControllerBase> logger)
    {
        _logger = logger;
    }

    [NonAction]
    protected IActionResult Ok<T>(T value)
    {
        var result = new Ok<object?>(value);
        return base.Ok(result);
    }

    [NonAction]
    protected new IActionResult Ok()
    {
        var result = new Ok();
        return base.Ok(result);
    }
}

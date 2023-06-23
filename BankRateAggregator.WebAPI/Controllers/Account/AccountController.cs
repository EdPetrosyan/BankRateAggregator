using BankRateAggregator.Application.UseCases.Account.Queries.GetAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankRateAggregator.WebAPI.Controllers.Account;

[Authorize]
public class AccountController : ApiControllerBase
{
    public AccountController(ILogger<ApiControllerBase> logger) : base(logger)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Guid id = new(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await Mediator.Send(new GetAccountQuery { Id = id }, cancellationToken);
        return Ok(result);
    }
}

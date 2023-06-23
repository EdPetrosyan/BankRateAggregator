using BankRateAggregator.Application.UseCases.Account.Commands.LoginAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankRateAggregator.WebAPI.Controllers.Account;

[AllowAnonymous]
public class LoginController : ApiControllerBase
{
    public LoginController(ILogger<ApiControllerBase> logger) : base(logger)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Post(LoginAccountCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}

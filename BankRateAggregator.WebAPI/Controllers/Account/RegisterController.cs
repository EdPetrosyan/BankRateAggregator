using BankRateAggregator.Application.UseCases.Register.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankRateAggregator.WebAPI.Controllers.Account;

[AllowAnonymous]
public class RegisterController : ApiControllerBase
{
    public RegisterController(ILogger<ApiControllerBase> logger) : base(logger)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Post(RegisterAccountCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

}

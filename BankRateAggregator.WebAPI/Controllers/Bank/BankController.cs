using BankRateAggregator.Application.UseCases.Bank.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankRateAggregator.WebAPI.Controllers.Bank
{
    [AllowAnonymous]
    public class BankController : ApiControllerBase
    {
        public BankController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }


        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            GetBanksQuery query = new();
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}

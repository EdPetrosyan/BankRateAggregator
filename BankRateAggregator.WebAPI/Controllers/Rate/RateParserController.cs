using BankRateAggregator.Application.UseCases.CurrencyParser.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankRateAggregator.WebAPI.Controllers.Rate
{
    public class RateParserController : ApiControllerBase
    {

        public RateParserController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(CancellationToken cancellationToken)
        {
            ParseCurrenciesCommand command = new();
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}

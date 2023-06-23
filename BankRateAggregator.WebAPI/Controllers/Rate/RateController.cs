using BankRateAggregator.Application.UseCases.Currency.Queries;
using BankRateAggregator.Application.UseCases.Rate.Commands;
using BankRateAggregator.Application.UseCases.Rate.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankRateAggregator.WebAPI.Controllers.Rate
{
    public class RateController : ApiControllerBase
    {
        public RateController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            GetRatesQuery query = new();
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("GetByBank/{bankId}")]
        public async Task<IActionResult> GetByBank([FromRoute] int bankId, CancellationToken cancellationToken)
        {
            GetRatesByBankQuery query = new()
            {
                BankId = bankId
            };
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("GetByCurrency/{currencyId}")]
        public async Task<IActionResult> GetByCurrency([FromRoute] int currencyId, CancellationToken cancellationToken)
        {
            GetRatesByCurrencyQuery query = new()
            {
                CurrencyId = currencyId
            };
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(AddRateCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

    }
}

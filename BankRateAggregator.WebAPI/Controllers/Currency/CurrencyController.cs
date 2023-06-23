using BankRateAggregator.Application.UseCases.Currency.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankRateAggregator.WebAPI.Controllers.Currency
{
    [AllowAnonymous]
    public class CurrencyController : ApiControllerBase
    {
        public CurrencyController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            GetCurrenciesQuery query = new();
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}

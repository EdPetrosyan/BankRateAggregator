using BankRateAggregator.Application.UseCases.Currency.Queries.Models;
using MediatR;

namespace BankRateAggregator.Application.UseCases.Rate.Queries
{
    public record GetRatesByCurrencyQuery : IRequest<List<RatesVM>>
    {
        public int CurrencyId { get; init; }
    }
}

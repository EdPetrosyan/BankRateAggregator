using BankRateAggregator.Application.UseCases.Currency.Queries.Models;
using MediatR;

namespace BankRateAggregator.Application.UseCases.Currency.Queries
{
    public record GetRatesQuery : IRequest<List<RatesVM>>
    {
    }
}

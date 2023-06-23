using BankRateAggregator.Application.UseCases.Currency.Queries.Models;
using MediatR;

namespace BankRateAggregator.Application.UseCases.Rate.Queries
{
    public record GetRatesByBankQuery : IRequest<List<RatesVM>>
    {
        public int BankId { get; init; }
    }
}

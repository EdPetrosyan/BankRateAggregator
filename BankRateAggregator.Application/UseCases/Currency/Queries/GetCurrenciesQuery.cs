using BankRateAggregator.Application.Services.Currency.Models;
using MediatR;

namespace BankRateAggregator.Application.UseCases.Currency.Queries
{
    public record GetCurrenciesQuery : IRequest<List<CurrencyIdValuePair>>
    {
    }
}

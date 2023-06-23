using MediatR;

namespace BankRateAggregator.Application.UseCases.CurrencyParser.Commands
{
    public record ParseCurrenciesCommand : IRequest
    {
    }
}

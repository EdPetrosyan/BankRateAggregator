using BankRateAggregator.Application.UseCases.Bank.Queries.Models;
using MediatR;

namespace BankRateAggregator.Application.UseCases.Bank.Queries
{
    public record GetBanksQuery() : IRequest<List<GetBankVM>>
    {
    }
}

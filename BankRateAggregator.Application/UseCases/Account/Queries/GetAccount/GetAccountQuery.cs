using BankRateAggregator.Application.UseCases.Account.Queries.GetAccount.Models;
using MediatR;

namespace BankRateAggregator.Application.UseCases.Account.Queries.GetAccount
{
    public record GetAccountQuery : IRequest<UserDto?>
    {
        public Guid Id { get; init; }
    }
}

using MediatR;

namespace BankRateAggregator.Application.UseCases.Account.Commands.LoginAccount;

public record LoginAccountCommand : IRequest<string>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

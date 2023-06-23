using MediatR;

namespace BankRateAggregator.Application.UseCases.Register.Commands
{
    public record RegisterAccountCommand : IRequest<string>
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string Password { get; init; } = string.Empty;
        public string ConfirmPassword { get; init; } = string.Empty;
    }
}

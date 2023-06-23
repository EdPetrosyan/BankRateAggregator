using MediatR;

namespace BankRateAggregator.Application.UseCases.Rate.Commands
{
    public record AddRateCommand : IRequest
    {
        public int BankId { get; init; }
        public int CurrencyId { get; init; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
    }
}

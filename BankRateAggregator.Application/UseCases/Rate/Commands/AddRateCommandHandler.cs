using BankRateAggregator.Application.Interfaces;
using MediatR;

namespace BankRateAggregator.Application.UseCases.Rate.Commands
{
    public class AddRateCommandHandler : IRequestHandler<AddRateCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddRateCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(AddRateCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Rates.AddAsync(new Domain.Entities.BankRates.Rate
            {
                BankId = request.BankId,
                CurrencyId = request.CurrencyId,
                Buy = request.Buy,
                Sell = request.Sell
            }, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.UseCases.Currency.Queries.Models;
using BankRateAggregator.Application.UseCases.Rate.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankRateAggregator.Application.UseCases.Currency.Queries
{
    public class GetRatesByBankQueryHandler : IRequestHandler<GetRatesByBankQuery, List<RatesVM>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetRatesByBankQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RatesVM>> Handle(GetRatesByBankQuery request, CancellationToken cancellationToken)
        {

            var latestRecords = await _dbContext.Rates
                .Where(x => x.BankId == request.BankId)
                .GroupBy(r => new { r.BankId, r.CurrencyId })
                .Select(g => g.Select(x => new RatesVM
                {
                    BankName = x.Bank.Name,
                    CurrencyName = x.Currency.Name,
                    Buy = x.Buy,
                    Sell = x.Sell,
                    LastUpdatedDate = x.Created
                }).OrderByDescending(x => x.LastUpdatedDate).First())
                .ToListAsync(cancellationToken);

            return latestRecords;

        }
    }

}

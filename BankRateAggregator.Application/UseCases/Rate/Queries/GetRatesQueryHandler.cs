using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.UseCases.Currency.Queries;
using BankRateAggregator.Application.UseCases.Currency.Queries.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankRateAggregator.Application.UseCases.Rate.Queries
{
    public class GetRatesQueryHandler : IRequestHandler<GetRatesQuery, List<RatesVM>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetRatesQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RatesVM>> Handle(GetRatesQuery request, CancellationToken cancellationToken)
        {

            var latestRecords = await _dbContext.Rates
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

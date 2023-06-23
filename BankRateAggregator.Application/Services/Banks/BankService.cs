using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.Services.Banks.Interfaces;
using BankRateAggregator.Application.Services.Banks.Models;
using Microsoft.EntityFrameworkCore;

namespace BankRateAggregator.Application.Services.Banks
{
    public class BankService : IBankService
    {
        private readonly IApplicationDbContext _dbContext;

        public BankService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BankVM>> GetBanksAsync(CancellationToken cancellationToken) => await _dbContext.Banks
                .Select(bank => new BankVM
                {
                    Id = bank.Id,
                    Name = bank.Name,
                    ApiUrl = bank.RateApiUrl,
                    Url = bank.WebSiteUrl,
                    XPath = bank.RateXPath
                }).ToListAsync(cancellationToken);
    }
}

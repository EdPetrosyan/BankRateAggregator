using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.Services.Currency.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankRateAggregator.Application.UseCases.Currency.Queries
{
    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, List<CurrencyIdValuePair>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetCurrenciesQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<CurrencyIdValuePair>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Currencies
                .Select(x => new CurrencyIdValuePair
                {
                    Id = x.Id,
                    Code = x.Code
                }).ToListAsync(cancellationToken);
        }
    }
}

using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.UseCases.Bank.Queries.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankRateAggregator.Application.UseCases.Bank.Queries
{
    public class GetBanksQueryHandler : IRequestHandler<GetBanksQuery, List<GetBankVM>>
    {
        private readonly IApplicationDbContext _dBcontext;

        public GetBanksQueryHandler(IApplicationDbContext dBcontext)
        {
            _dBcontext = dBcontext;
        }

        public Task<List<GetBankVM>> Handle(GetBanksQuery request, CancellationToken cancellationToken)
        {
            return _dBcontext.Banks
                .Select(x => new GetBankVM
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync(cancellationToken);
        }
    }
}

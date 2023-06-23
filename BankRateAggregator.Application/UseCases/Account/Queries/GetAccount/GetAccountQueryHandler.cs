using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.UseCases.Account.Queries.GetAccount.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankRateAggregator.Application.UseCases.Account.Queries.GetAccount
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, UserDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetAccountQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Users
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return result is null ? null : new UserDto
            {
                Email = result.Email,
                Name = result.Name,
                LastName = result.LastName,
                PhoneNumber = result.PhoneNumber,
                BirthDate = result.BirthDate
            };
        }
    }
}

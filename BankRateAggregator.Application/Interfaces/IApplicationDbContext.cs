using BankRateAggregator.Domain.Entities.BankRates;
using BankRateAggregator.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankRateAggregator.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<RoleClaim> RoleClaims { get; }
        DbSet<Bank> Banks { get; }
        DbSet<Currency> Currencies { get; }
        DbSet<Rate> Rates { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

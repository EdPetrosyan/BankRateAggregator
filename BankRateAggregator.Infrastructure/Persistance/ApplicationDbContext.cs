using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Domain.Common;
using BankRateAggregator.Domain.Entities.BankRates;
using BankRateAggregator.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BankRateAggregator.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
               ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        #region DbSets Init
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RoleClaim> RoleClaims => Set<RoleClaim>();
        public DbSet<Bank> Banks => Set<Bank>();
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<Rate> Rates => Set<Rate>();

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateEntities()
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTimeOffset.UtcNow;
                        break;
                }
            }
        }
    }
}

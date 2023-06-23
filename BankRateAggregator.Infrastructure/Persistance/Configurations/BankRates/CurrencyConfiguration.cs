using BankRateAggregator.Domain.Entities.BankRates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRateAggregator.Infrastructure.Persistance.Configurations.BankRates
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).
                HasMaxLength(3);

            builder.Property(x => x.Alias).
                HasMaxLength(3);

            builder.Property(x => x.Name).
                HasMaxLength(32);
        }
    }
}

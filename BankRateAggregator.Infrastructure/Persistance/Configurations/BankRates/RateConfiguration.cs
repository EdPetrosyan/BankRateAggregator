using BankRateAggregator.Domain.Entities.BankRates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRateAggregator.Infrastructure.Persistance.Configurations.BankRates
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Bank).
                WithMany(x => x.Rates).
                HasForeignKey(x => x.BankId);

            builder.HasOne(x => x.Currency).
                WithMany(x => x.Rates).
                HasForeignKey(x => x.CurrencyId);
        }
    }
}

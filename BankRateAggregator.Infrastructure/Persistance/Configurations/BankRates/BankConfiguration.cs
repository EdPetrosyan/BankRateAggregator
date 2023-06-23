using BankRateAggregator.Domain.Entities.BankRates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRateAggregator.Infrastructure.Persistance.Configurations.BankRates
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RateApiUrl).
                HasMaxLength(512);

            builder.Property(x => x.RateXPath).
                HasMaxLength(512);

            builder.Property(x => x.Name).
                HasMaxLength(64);

            builder.Property(x => x.WebSiteUrl)
                .HasMaxLength(256);
        }
    }
}

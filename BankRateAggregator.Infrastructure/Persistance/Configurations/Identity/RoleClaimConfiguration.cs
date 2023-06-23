using BankRateAggregator.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRateAggregator.Infrastructure.Persistance.Configurations.Identity;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClaimType)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.ClaimValue)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.RoleClaims)
            .HasForeignKey(x => x.RoleId);

    }
}
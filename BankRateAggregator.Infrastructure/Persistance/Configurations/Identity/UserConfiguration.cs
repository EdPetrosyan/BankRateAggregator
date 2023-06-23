using BankRateAggregator.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRateAggregator.Infrastructure.Persistance.Configurations.Identity;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(36);

        builder.HasIndex(e => e.UserName)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.LastName)
            .HasMaxLength(128);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(16);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(160)
            .IsRequired();
    }
}

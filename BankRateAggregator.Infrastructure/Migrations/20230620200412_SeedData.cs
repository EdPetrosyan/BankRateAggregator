using BankRateAggregator.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankRateAggregator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                   table: nameof(Role) + "s",
                   columns: new[] { nameof(Role.Name), nameof(Role.Created), nameof(Role.CreatedBy), nameof(Role.LastModified), nameof(Role.LastModifiedBy) },
                   values: new object[,]
                   {
                                    { "Administrator",DateTimeOffset.UtcNow, "system",DateTimeOffset.UtcNow, "system" },
                                    { "User",DateTimeOffset.UtcNow, "system",DateTimeOffset.UtcNow, "system" }
                   });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

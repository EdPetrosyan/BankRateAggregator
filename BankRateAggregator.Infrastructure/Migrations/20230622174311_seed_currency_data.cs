using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankRateAggregator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed_currency_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO public.""Currencies"" (""Name"", ""Code"", ""Alias"", ""Created"", ""CreatedBy"", ""LastModified"", ""LastModifiedBy"" )
                                    VALUES
                                        ('US Dollar', 'USD', null, CURRENT_DATE, 'script', CURRENT_DATE, 'script'),
                                        ('Euro', 'EUR', null, CURRENT_DATE, 'script', CURRENT_DATE, 'script'),
                                        ('Ruble', 'RUR', 'RUB', CURRENT_DATE, 'script', CURRENT_DATE, 'script'),
                                        ('GB Pound', 'GBP', null, CURRENT_DATE, 'script', CURRENT_DATE, 'script'),
                                        ('Lari', 'GEL', null, CURRENT_DATE, 'script', CURRENT_DATE, 'script'),
                                        ('Swiss Franc', 'CHF', null, CURRENT_DATE, 'script', CURRENT_DATE, 'script'),
                                        ('Yuan', 'CNY', null, CURRENT_DATE, 'script', CURRENT_DATE, 'script'),
                                        ('Canadian Dollar', 'CAD', null, CURRENT_DATE, 'script', CURRENT_DATE, 'script'),
                                        ('Australian Dollar', 'AUD', null, CURRENT_DATE, 'script', CURRENT_DATE, 'script'),
                                        ('Dirham', 'AED', null, CURRENT_DATE, 'script', CURRENT_DATE, 'script')
                                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

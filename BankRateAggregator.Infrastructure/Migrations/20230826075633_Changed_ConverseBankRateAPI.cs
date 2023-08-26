using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankRateAggregator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changed_ConverseBankRateAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"	Update public.""Banks""
	            Set ""RateApiUrl"" = 'https://sapi.conversebank.am/api/v2/currencyrates'
	            WHERE ""Name"" = 'Converse Bank'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankRateAggregator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changed_AAB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"	Update public.""Banks""
	            Set ""RateXPath"" = '/html/body/div/main/div/div/div/section[3]/table[2]', ""RateApiUrl"" = NULL
	            WHERE ""Name"" = 'ArmBusinessBank';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

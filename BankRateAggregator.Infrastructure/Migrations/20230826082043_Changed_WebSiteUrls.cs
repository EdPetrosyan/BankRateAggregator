using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankRateAggregator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changed_WebSiteUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"	Update public.""Banks""
	                Set ""WebSiteUrl"" = 'https://www.inecobank.am'
	                WHERE ""Name"" = 'Inecobank';
	
	                Update public.""Banks""
	                Set ""WebSiteUrl"" = 'https://www.artsakhbank.am'
	                WHERE ""Name"" = 'Artsakhbank';
	                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

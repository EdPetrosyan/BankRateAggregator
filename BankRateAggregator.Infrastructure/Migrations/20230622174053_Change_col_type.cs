using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankRateAggregator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_col_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ALias",
                table: "Currencies",
                newName: "Alias");

            migrationBuilder.AlterColumn<string>(
                name: "Alias",
                table: "Currencies",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(3)",
                oldMaxLength: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Alias",
                table: "Currencies",
                newName: "ALias");

            migrationBuilder.AlterColumn<string>(
                name: "ALias",
                table: "Currencies",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(3)",
                oldMaxLength: 3,
                oldNullable: true);
        }
    }
}

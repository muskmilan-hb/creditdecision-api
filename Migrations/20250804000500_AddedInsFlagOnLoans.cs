using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditDecision.Migrations
{
    /// <inheritdoc />
    public partial class AddedInsFlagOnLoans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasInsurance",
                table: "Loans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasInsurance",
                table: "Loans");
        }
    }
}

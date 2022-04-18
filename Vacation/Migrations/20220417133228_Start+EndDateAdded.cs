using Microsoft.EntityFrameworkCore.Migrations;

namespace Vacation.Migrations
{
    public partial class StartEndDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Vacations");

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Vacations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Vacations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Vacations");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

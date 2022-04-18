using Microsoft.EntityFrameworkCore.Migrations;

namespace Vacation.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Vacations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmployeeID",
                table: "Vacations",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_Employees_EmployeeID",
                table: "Vacations",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_Employees_EmployeeID",
                table: "Vacations");

            migrationBuilder.DropIndex(
                name: "IX_Vacations_EmployeeID",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

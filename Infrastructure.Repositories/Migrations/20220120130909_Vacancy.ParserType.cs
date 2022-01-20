using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitingStaff.Infrastructure.Repositories.Migrations
{
    public partial class VacancyParserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParserType",
                table: "Vacancy",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "47143d01-b1df-463b-a8ec-759032471b8e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParserType",
                table: "Vacancy");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7af0ec86-3810-4dde-afc8-d4a002f9a320");
        }
    }
}

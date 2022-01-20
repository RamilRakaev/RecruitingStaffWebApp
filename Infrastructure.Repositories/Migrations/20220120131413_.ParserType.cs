using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitingStaff.Infrastructure.Repositories.Migrations
{
    public partial class ParserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParserType",
                table: "Vacancy");

            migrationBuilder.AddColumn<int>(
                name: "ParserType",
                table: "Questionnaire",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b3d90b28-0952-465d-b2d0-14649ba473ca");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParserType",
                table: "Questionnaire");

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
    }
}

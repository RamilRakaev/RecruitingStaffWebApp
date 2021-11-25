using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class ChangeDateFieldsFromEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfStart",
                table: "Education",
                newName: "StartDateOfTraining");

            migrationBuilder.RenameColumn(
                name: "DateOfEnd",
                table: "Education",
                newName: "EndDateOfTraining");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7c9a0554-6022-4e50-be7c-288dfcf33ac9");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateOfTraining",
                table: "Education",
                newName: "DateOfStart");

            migrationBuilder.RenameColumn(
                name: "EndDateOfTraining",
                table: "Education",
                newName: "DateOfEnd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ce8f81f7-4fe0-4a4f-afc6-70ee08683ab2");
        }
    }
}

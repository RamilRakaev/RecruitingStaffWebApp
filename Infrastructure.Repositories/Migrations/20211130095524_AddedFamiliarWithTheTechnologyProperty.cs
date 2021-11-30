using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class AddedFamiliarWithTheTechnologyProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specification",
                table: "Education",
                newName: "Specialization");

            migrationBuilder.AddColumn<string>(
                name: "FamiliarWithTheTechnology",
                table: "Answer",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "23475e65-4b65-4ebf-bb08-9f9db3d860af");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamiliarWithTheTechnology",
                table: "Answer");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Education",
                newName: "Specification");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7c9a0554-6022-4e50-be7c-288dfcf33ac9");
        }
    }
}

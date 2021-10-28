using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContenderId",
                table: "Options",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9d475585-21eb-474b-81a2-af4509762aa5");

            migrationBuilder.CreateIndex(
                name: "IX_Options_ContenderId",
                table: "Options",
                column: "ContenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Contenders_ContenderId",
                table: "Options",
                column: "ContenderId",
                principalTable: "Contenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Contenders_ContenderId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_ContenderId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ContenderId",
                table: "Options");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5162cb7b-db37-44d9-9db8-1c5b463b3e15");
        }
    }
}

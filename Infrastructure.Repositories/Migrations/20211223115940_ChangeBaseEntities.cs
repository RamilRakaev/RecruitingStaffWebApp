using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class ChangeBaseEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dd98b64c-f4eb-413b-821e-5c2eadd58e7f");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "137d12d1-2dca-4221-9366-9fc1c2decf07");
        }
    }
}

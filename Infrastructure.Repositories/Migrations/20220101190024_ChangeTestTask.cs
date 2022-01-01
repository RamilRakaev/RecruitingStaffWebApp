using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class ChangeTestTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecruitingStaffWebAppFile_TestTaskId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "98f7d176-ba07-4740-a403-b8d3b958747e");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitingStaffWebAppFile_TestTaskId",
                table: "RecruitingStaffWebAppFile",
                column: "TestTaskId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecruitingStaffWebAppFile_TestTaskId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b896ecf6-dc27-42f3-acdc-dc8afc808294");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitingStaffWebAppFile_TestTaskId",
                table: "RecruitingStaffWebAppFile",
                column: "TestTaskId");
        }
    }
}

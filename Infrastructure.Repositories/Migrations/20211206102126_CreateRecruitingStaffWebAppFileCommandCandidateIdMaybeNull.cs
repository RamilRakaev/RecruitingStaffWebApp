using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class CreateRecruitingStaffWebAppFileCommandCandidateIdMaybeNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Candidate_CandidateId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "RecruitingStaffWebAppFile",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "430637d6-e799-4e53-af5f-fb143ba960d0");

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Candidate_CandidateId",
                table: "RecruitingStaffWebAppFile",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Candidate_CandidateId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "RecruitingStaffWebAppFile",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "09ea4b4a-7549-418b-a780-27ff3bca9067");

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Candidate_CandidateId",
                table: "RecruitingStaffWebAppFile",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

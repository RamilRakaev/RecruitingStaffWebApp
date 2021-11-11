using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class RecruitingStaffWebAppFileQuestionnaireId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Questionnaire_QuestionnaireId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionnaireId",
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
                value: "adbb12fd-23ad-4cd2-b61a-27692b14fb95");

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Questionnaire_QuestionnaireId",
                table: "RecruitingStaffWebAppFile",
                column: "QuestionnaireId",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Questionnaire_QuestionnaireId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionnaireId",
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
                value: "c2b0f285-b83c-477e-bab1-26e7d05749e3");

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Questionnaire_QuestionnaireId",
                table: "RecruitingStaffWebAppFile",
                column: "QuestionnaireId",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

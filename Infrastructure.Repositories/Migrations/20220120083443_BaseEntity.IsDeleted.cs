using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitingStaff.Infrastructure.Repositories.Migrations
{
    public partial class BaseEntityIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VacancyQuestionnaire",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vacancy",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TestTask",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RecruitingStaffWebAppFile",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Recommender",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Questionnaire",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuestionCategory",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Question",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PreviousJobPlacement",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Option",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Kid",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Education",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CandidateVacancy",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CandidateTestTask",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CandidateQuestionnaire",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Candidate",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Answer",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7af0ec86-3810-4dde-afc8-d4a002f9a320");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VacancyQuestionnaire");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Recommender");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Questionnaire");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuestionCategory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PreviousJobPlacement");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Option");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Kid");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CandidateVacancy");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CandidateTestTask");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Answer");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7f34c9ee-6d89-4a7e-99a2-7cb48c61c0a2");
        }
    }
}

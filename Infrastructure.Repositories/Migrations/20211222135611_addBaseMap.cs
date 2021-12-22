using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Repositories.Migrations
{
    public partial class addBaseMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_CandidatesId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_QuestionnairesId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Vacancy_VacanciesId",
                table: "CandidateVacancy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateVacancy",
                table: "CandidateVacancy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateQuestionnaire",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Answer");

            migrationBuilder.RenameColumn(
                name: "VacanciesId",
                table: "CandidateVacancy",
                newName: "VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateVacancy_VacanciesId",
                table: "CandidateVacancy",
                newName: "IX_CandidateVacancy_VacancyId");

            migrationBuilder.RenameColumn(
                name: "QuestionnairesId",
                table: "CandidateQuestionnaire",
                newName: "QuestionnaireId");

            migrationBuilder.RenameColumn(
                name: "CandidatesId",
                table: "CandidateQuestionnaire",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateQuestionnaire_QuestionnairesId",
                table: "CandidateQuestionnaire",
                newName: "IX_CandidateQuestionnaire_QuestionnaireId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CandidateVacancy",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CandidateQuestionnaire",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateVacancy",
                table: "CandidateVacancy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateQuestionnaire",
                table: "CandidateQuestionnaire",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9fb90381-f8ee-441b-a13f-0703b1ea4012");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateVacancy_CandidateId",
                table: "CandidateVacancy",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateQuestionnaire_CandidateId",
                table: "CandidateQuestionnaire",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_CandidateId",
                table: "CandidateQuestionnaire",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_QuestionnaireId",
                table: "CandidateQuestionnaire",
                column: "QuestionnaireId",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancy_Vacancy_VacancyId",
                table: "CandidateVacancy",
                column: "VacancyId",
                principalTable: "Vacancy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_CandidateId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_QuestionnaireId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Vacancy_VacancyId",
                table: "CandidateVacancy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateVacancy",
                table: "CandidateVacancy");

            migrationBuilder.DropIndex(
                name: "IX_CandidateVacancy_CandidateId",
                table: "CandidateVacancy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateQuestionnaire",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropIndex(
                name: "IX_CandidateQuestionnaire_CandidateId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CandidateVacancy");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CandidateQuestionnaire");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "CandidateVacancy",
                newName: "VacanciesId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateVacancy_VacancyId",
                table: "CandidateVacancy",
                newName: "IX_CandidateVacancy_VacanciesId");

            migrationBuilder.RenameColumn(
                name: "QuestionnaireId",
                table: "CandidateQuestionnaire",
                newName: "QuestionnairesId");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "CandidateQuestionnaire",
                newName: "CandidatesId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateQuestionnaire_QuestionnaireId",
                table: "CandidateQuestionnaire",
                newName: "IX_CandidateQuestionnaire_QuestionnairesId");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Answer",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateVacancy",
                table: "CandidateVacancy",
                columns: new[] { "CandidateId", "VacanciesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateQuestionnaire",
                table: "CandidateQuestionnaire",
                columns: new[] { "CandidatesId", "QuestionnairesId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "acdf38d2-dd95-404d-971a-ad2fbc8a76cf");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_CandidatesId",
                table: "CandidateQuestionnaire",
                column: "CandidatesId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_QuestionnairesId",
                table: "CandidateQuestionnaire",
                column: "QuestionnairesId",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancy_Vacancy_VacanciesId",
                table: "CandidateVacancy",
                column: "VacanciesId",
                principalTable: "Vacancy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

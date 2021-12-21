using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class ChangeCandidateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_CandidateQuestionnairesId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_CandidateQuestionnaire~",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Candidate_CandidateVacanciesId",
                table: "CandidateVacancy");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Vacancy_CandidateVacanciesId1",
                table: "CandidateVacancy");

            migrationBuilder.RenameColumn(
                name: "CandidateVacanciesId1",
                table: "CandidateVacancy",
                newName: "VacanciesId");

            migrationBuilder.RenameColumn(
                name: "CandidateVacanciesId",
                table: "CandidateVacancy",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateVacancy_CandidateVacanciesId1",
                table: "CandidateVacancy",
                newName: "IX_CandidateVacancy_VacanciesId");

            migrationBuilder.RenameColumn(
                name: "CandidateQuestionnairesId1",
                table: "CandidateQuestionnaire",
                newName: "QuestionnairesId");

            migrationBuilder.RenameColumn(
                name: "CandidateQuestionnairesId",
                table: "CandidateQuestionnaire",
                newName: "CandidatesId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateQuestionnaire_CandidateQuestionnairesId1",
                table: "CandidateQuestionnaire",
                newName: "IX_CandidateQuestionnaire_QuestionnairesId");

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
                name: "FK_CandidateVacancy_Candidate_CandidateId",
                table: "CandidateVacancy",
                column: "CandidateId",
                principalTable: "Candidate",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_CandidatesId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_QuestionnairesId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Candidate_CandidateId",
                table: "CandidateVacancy");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Vacancy_VacanciesId",
                table: "CandidateVacancy");

            migrationBuilder.RenameColumn(
                name: "VacanciesId",
                table: "CandidateVacancy",
                newName: "CandidateVacanciesId1");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "CandidateVacancy",
                newName: "CandidateVacanciesId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateVacancy_VacanciesId",
                table: "CandidateVacancy",
                newName: "IX_CandidateVacancy_CandidateVacanciesId1");

            migrationBuilder.RenameColumn(
                name: "QuestionnairesId",
                table: "CandidateQuestionnaire",
                newName: "CandidateQuestionnairesId1");

            migrationBuilder.RenameColumn(
                name: "CandidatesId",
                table: "CandidateQuestionnaire",
                newName: "CandidateQuestionnairesId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateQuestionnaire_QuestionnairesId",
                table: "CandidateQuestionnaire",
                newName: "IX_CandidateQuestionnaire_CandidateQuestionnairesId1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e96941de-be98-4a11-a5a2-0e53172adcd7");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_CandidateQuestionnairesId",
                table: "CandidateQuestionnaire",
                column: "CandidateQuestionnairesId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_CandidateQuestionnaire~",
                table: "CandidateQuestionnaire",
                column: "CandidateQuestionnairesId1",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancy_Candidate_CandidateVacanciesId",
                table: "CandidateVacancy",
                column: "CandidateVacanciesId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancy_Vacancy_CandidateVacanciesId1",
                table: "CandidateVacancy",
                column: "CandidateVacanciesId1",
                principalTable: "Vacancy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

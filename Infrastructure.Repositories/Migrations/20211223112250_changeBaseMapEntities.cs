using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class changeBaseMapEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_CandidateId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_QuestionnaireId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Candidate_CandidateId",
                table: "CandidateVacancy");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Vacancy_VacancyId",
                table: "CandidateVacancy");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "CandidateVacancy",
                newName: "SecondEntityId");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "CandidateVacancy",
                newName: "FirstEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateVacancy_VacancyId",
                table: "CandidateVacancy",
                newName: "IX_CandidateVacancy_SecondEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateVacancy_CandidateId",
                table: "CandidateVacancy",
                newName: "IX_CandidateVacancy_FirstEntityId");

            migrationBuilder.RenameColumn(
                name: "QuestionnaireId",
                table: "CandidateQuestionnaire",
                newName: "SecondEntityId");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "CandidateQuestionnaire",
                newName: "FirstEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateQuestionnaire_QuestionnaireId",
                table: "CandidateQuestionnaire",
                newName: "IX_CandidateQuestionnaire_SecondEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateQuestionnaire_CandidateId",
                table: "CandidateQuestionnaire",
                newName: "IX_CandidateQuestionnaire_FirstEntityId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "137d12d1-2dca-4221-9366-9fc1c2decf07");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_FirstEntityId",
                table: "CandidateQuestionnaire",
                column: "FirstEntityId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_SecondEntityId",
                table: "CandidateQuestionnaire",
                column: "SecondEntityId",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancy_Candidate_FirstEntityId",
                table: "CandidateVacancy",
                column: "FirstEntityId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancy_Vacancy_SecondEntityId",
                table: "CandidateVacancy",
                column: "SecondEntityId",
                principalTable: "Vacancy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Candidate_FirstEntityId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaire_SecondEntityId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Candidate_FirstEntityId",
                table: "CandidateVacancy");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Vacancy_SecondEntityId",
                table: "CandidateVacancy");

            migrationBuilder.RenameColumn(
                name: "SecondEntityId",
                table: "CandidateVacancy",
                newName: "VacancyId");

            migrationBuilder.RenameColumn(
                name: "FirstEntityId",
                table: "CandidateVacancy",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateVacancy_SecondEntityId",
                table: "CandidateVacancy",
                newName: "IX_CandidateVacancy_VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateVacancy_FirstEntityId",
                table: "CandidateVacancy",
                newName: "IX_CandidateVacancy_CandidateId");

            migrationBuilder.RenameColumn(
                name: "SecondEntityId",
                table: "CandidateQuestionnaire",
                newName: "QuestionnaireId");

            migrationBuilder.RenameColumn(
                name: "FirstEntityId",
                table: "CandidateQuestionnaire",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateQuestionnaire_SecondEntityId",
                table: "CandidateQuestionnaire",
                newName: "IX_CandidateQuestionnaire_QuestionnaireId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateQuestionnaire_FirstEntityId",
                table: "CandidateQuestionnaire",
                newName: "IX_CandidateQuestionnaire_CandidateId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9fb90381-f8ee-441b-a13f-0703b1ea4012");

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
                name: "FK_CandidateVacancy_Candidate_CandidateId",
                table: "CandidateVacancy",
                column: "CandidateId",
                principalTable: "Candidate",
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
    }
}

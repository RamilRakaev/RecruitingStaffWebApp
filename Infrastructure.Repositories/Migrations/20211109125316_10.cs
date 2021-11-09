using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Candidates_CandidateId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Candidates_CandidateId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaires_QuestionnaireId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Files_PhotoId1",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Candidates_CandidateId",
                table: "CandidateVacancy");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancy_Vacancies_VacancyId",
                table: "CandidateVacancy");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Candidates_CandidateId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Questionnaires_QuestionnaireId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Candidates_CandidateId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCategories_Questionnaires_QuestionnaireId",
                table: "QuestionCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaires_Vacancies_VacancyId",
                table: "Questionnaires");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionCategories_QuestionCategoryId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vacancies",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questionnaires",
                table: "Questionnaires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionCategories",
                table: "QuestionCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "Vacancies",
                newName: "Vacancy");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "Questionnaires",
                newName: "Questionnaire");

            migrationBuilder.RenameTable(
                name: "QuestionCategories",
                newName: "QuestionCategory");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "Option");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "RecruitingStaffWebAppFile");

            migrationBuilder.RenameTable(
                name: "Candidates",
                newName: "Candidate");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuestionCategoryId",
                table: "Question",
                newName: "IX_Question_QuestionCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Questionnaires_VacancyId",
                table: "Questionnaire",
                newName: "IX_Questionnaire_VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionCategories_QuestionnaireId",
                table: "QuestionCategory",
                newName: "IX_QuestionCategory_QuestionnaireId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_CandidateId",
                table: "Option",
                newName: "IX_Option_CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_QuestionnaireId",
                table: "RecruitingStaffWebAppFile",
                newName: "IX_RecruitingStaffWebAppFile_QuestionnaireId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_CandidateId",
                table: "RecruitingStaffWebAppFile",
                newName: "IX_RecruitingStaffWebAppFile_CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_PhotoId1",
                table: "Candidate",
                newName: "IX_Candidate_PhotoId1");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionId",
                table: "Answer",
                newName: "IX_Answer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_CandidateId",
                table: "Answer",
                newName: "IX_Answer_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vacancy",
                table: "Vacancy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questionnaire",
                table: "Questionnaire",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionCategory",
                table: "QuestionCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Option",
                table: "Option",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecruitingStaffWebAppFile",
                table: "RecruitingStaffWebAppFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidate",
                table: "Candidate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c2b0f285-b83c-477e-bab1-26e7d05749e3");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Candidate_CandidateId",
                table: "Answer",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_RecruitingStaffWebAppFile_PhotoId1",
                table: "Candidate",
                column: "PhotoId1",
                principalTable: "RecruitingStaffWebAppFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Option_Candidate_CandidateId",
                table: "Option",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionCategory_QuestionCategoryId",
                table: "Question",
                column: "QuestionCategoryId",
                principalTable: "QuestionCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCategory_Questionnaire_QuestionnaireId",
                table: "QuestionCategory",
                column: "QuestionnaireId",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaire_Vacancy_VacancyId",
                table: "Questionnaire",
                column: "VacancyId",
                principalTable: "Vacancy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Candidate_CandidateId",
                table: "RecruitingStaffWebAppFile",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Questionnaire_QuestionnaireId",
                table: "RecruitingStaffWebAppFile",
                column: "QuestionnaireId",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Candidate_CandidateId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_RecruitingStaffWebAppFile_PhotoId1",
                table: "Candidate");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Option_Candidate_CandidateId",
                table: "Option");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionCategory_QuestionCategoryId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCategory_Questionnaire_QuestionnaireId",
                table: "QuestionCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaire_Vacancy_VacancyId",
                table: "Questionnaire");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Candidate_CandidateId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitingStaffWebAppFile_Questionnaire_QuestionnaireId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vacancy",
                table: "Vacancy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecruitingStaffWebAppFile",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questionnaire",
                table: "Questionnaire");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionCategory",
                table: "QuestionCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Option",
                table: "Option");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidate",
                table: "Candidate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "Vacancy",
                newName: "Vacancies");

            migrationBuilder.RenameTable(
                name: "RecruitingStaffWebAppFile",
                newName: "Files");

            migrationBuilder.RenameTable(
                name: "Questionnaire",
                newName: "Questionnaires");

            migrationBuilder.RenameTable(
                name: "QuestionCategory",
                newName: "QuestionCategories");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "Option",
                newName: "Options");

            migrationBuilder.RenameTable(
                name: "Candidate",
                newName: "Candidates");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_RecruitingStaffWebAppFile_QuestionnaireId",
                table: "Files",
                newName: "IX_Files_QuestionnaireId");

            migrationBuilder.RenameIndex(
                name: "IX_RecruitingStaffWebAppFile_CandidateId",
                table: "Files",
                newName: "IX_Files_CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Questionnaire_VacancyId",
                table: "Questionnaires",
                newName: "IX_Questionnaires_VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionCategory_QuestionnaireId",
                table: "QuestionCategories",
                newName: "IX_QuestionCategories_QuestionnaireId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuestionCategoryId",
                table: "Questions",
                newName: "IX_Questions_QuestionCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Option_CandidateId",
                table: "Options",
                newName: "IX_Options_CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidate_PhotoId1",
                table: "Candidates",
                newName: "IX_Candidates_PhotoId1");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionId",
                table: "Answers",
                newName: "IX_Answers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_CandidateId",
                table: "Answers",
                newName: "IX_Answers_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vacancies",
                table: "Vacancies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questionnaires",
                table: "Questionnaires",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionCategories",
                table: "QuestionCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c3a087de-fbd6-4507-904a-6e17674a8fee");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Candidates_CandidateId",
                table: "Answers",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Candidates_CandidateId",
                table: "CandidateQuestionnaire",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateQuestionnaire_Questionnaires_QuestionnaireId",
                table: "CandidateQuestionnaire",
                column: "QuestionnaireId",
                principalTable: "Questionnaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Files_PhotoId1",
                table: "Candidates",
                column: "PhotoId1",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancy_Candidates_CandidateId",
                table: "CandidateVacancy",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancy_Vacancies_VacancyId",
                table: "CandidateVacancy",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Candidates_CandidateId",
                table: "Files",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Questionnaires_QuestionnaireId",
                table: "Files",
                column: "QuestionnaireId",
                principalTable: "Questionnaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Candidates_CandidateId",
                table: "Options",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCategories_Questionnaires_QuestionnaireId",
                table: "QuestionCategories",
                column: "QuestionnaireId",
                principalTable: "Questionnaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaires_Vacancies_VacancyId",
                table: "Questionnaires",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionCategories_QuestionCategoryId",
                table: "Questions",
                column: "QuestionCategoryId",
                principalTable: "QuestionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

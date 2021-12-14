using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Repositories.Migrations
{
    public partial class AddPropertyNameInBaseEntity : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Recommender_PreviousJob_PreviousJobId",
                table: "Recommender");

            migrationBuilder.DropTable(
                name: "PreviousJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateVacancy",
                table: "CandidateVacancy");

            migrationBuilder.DropIndex(
                name: "IX_CandidateVacancy_VacancyId",
                table: "CandidateVacancy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateQuestionnaire",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropIndex(
                name: "IX_CandidateQuestionnaire_QuestionnaireId",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "CandidateVacancy");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "CandidateQuestionnaire");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "RecruitingStaffWebAppFile",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Recommender",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PropertyName",
                table: "Option",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "EducationalInstitutionName",
                table: "Education",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CandidateVacancy",
                newName: "CandidateVacanciesId1");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "CandidateVacancy",
                newName: "CandidateVacanciesId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CandidateQuestionnaire",
                newName: "CandidateQuestionnairesId1");

            migrationBuilder.RenameColumn(
                name: "QuestionnaireId",
                table: "CandidateQuestionnaire",
                newName: "CandidateQuestionnairesId");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Candidate",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Kid",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Answer",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateVacancy",
                table: "CandidateVacancy",
                columns: new[] { "CandidateVacanciesId", "CandidateVacanciesId1" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateQuestionnaire",
                table: "CandidateQuestionnaire",
                columns: new[] { "CandidateQuestionnairesId", "CandidateQuestionnairesId1" });

            migrationBuilder.CreateTable(
                name: "PreviousJobPlacement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    PositionAtWork = table.Column<string>(type: "text", nullable: true),
                    Salary = table.Column<string>(type: "text", nullable: true),
                    DateOfStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Responsibilities = table.Column<string>(type: "text", nullable: true),
                    LeavingReason = table.Column<string>(type: "text", nullable: true),
                    CandidateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousJobPlacement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreviousJobPlacement_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e96941de-be98-4a11-a5a2-0e53172adcd7");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateVacancy_CandidateVacanciesId1",
                table: "CandidateVacancy",
                column: "CandidateVacanciesId1");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateQuestionnaire_CandidateQuestionnairesId1",
                table: "CandidateQuestionnaire",
                column: "CandidateQuestionnairesId1");

            migrationBuilder.CreateIndex(
                name: "IX_PreviousJobPlacement_CandidateId",
                table: "PreviousJobPlacement",
                column: "CandidateId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Recommender_PreviousJobPlacement_PreviousJobId",
                table: "Recommender",
                column: "PreviousJobId",
                principalTable: "PreviousJobPlacement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Recommender_PreviousJobPlacement_PreviousJobId",
                table: "Recommender");

            migrationBuilder.DropTable(
                name: "PreviousJobPlacement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateVacancy",
                table: "CandidateVacancy");

            migrationBuilder.DropIndex(
                name: "IX_CandidateVacancy_CandidateVacanciesId1",
                table: "CandidateVacancy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateQuestionnaire",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropIndex(
                name: "IX_CandidateQuestionnaire_CandidateQuestionnairesId1",
                table: "CandidateQuestionnaire");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Kid");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Answer");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RecruitingStaffWebAppFile",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Recommender",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Option",
                newName: "PropertyName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Education",
                newName: "EducationalInstitutionName");

            migrationBuilder.RenameColumn(
                name: "CandidateVacanciesId1",
                table: "CandidateVacancy",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CandidateVacanciesId",
                table: "CandidateVacancy",
                newName: "VacancyId");

            migrationBuilder.RenameColumn(
                name: "CandidateQuestionnairesId1",
                table: "CandidateQuestionnaire",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CandidateQuestionnairesId",
                table: "CandidateQuestionnaire",
                newName: "QuestionnaireId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Candidate",
                newName: "FullName");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "CandidateVacancy",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "CandidateQuestionnaire",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateVacancy",
                table: "CandidateVacancy",
                columns: new[] { "CandidateId", "VacancyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateQuestionnaire",
                table: "CandidateQuestionnaire",
                columns: new[] { "CandidateId", "QuestionnaireId" });

            migrationBuilder.CreateTable(
                name: "PreviousJob",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CandidateId = table.Column<int>(type: "integer", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LeavingReason = table.Column<string>(type: "text", nullable: true),
                    OrganizationAddress = table.Column<string>(type: "text", nullable: true),
                    OrganizationName = table.Column<string>(type: "text", nullable: true),
                    OrganizationPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PositionAtWork = table.Column<string>(type: "text", nullable: true),
                    Responsibilities = table.Column<string>(type: "text", nullable: true),
                    Salary = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreviousJob_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "430637d6-e799-4e53-af5f-fb143ba960d0");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateVacancy_VacancyId",
                table: "CandidateVacancy",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateQuestionnaire_QuestionnaireId",
                table: "CandidateQuestionnaire",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_PreviousJob_CandidateId",
                table: "PreviousJob",
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
                name: "FK_Recommender_PreviousJob_PreviousJobId",
                table: "Recommender",
                column: "PreviousJobId",
                principalTable: "PreviousJob",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

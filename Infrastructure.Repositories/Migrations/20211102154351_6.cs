using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Repositories.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Questionnaire_QuestionnaireId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Vacancies_VacancyId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaire_Vacancies_VacancyId",
                table: "Questionnaire");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_QuestionnaireId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_VacancyId",
                table: "Candidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questionnaire",
                table: "Questionnaire");

            migrationBuilder.DropColumn(
                name: "QuestionnaireId",
                table: "Candidates");

            migrationBuilder.RenameTable(
                name: "Questionnaire",
                newName: "Questionnaires");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "Candidates",
                newName: "PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_Questionnaire_VacancyId",
                table: "Questionnaires",
                newName: "IX_Questionnaires_VacancyId");

            migrationBuilder.AlterColumn<int>(
                name: "VacancyId",
                table: "Questionnaires",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Questionnaires",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentFileId",
                table: "Questionnaires",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questionnaires",
                table: "Questionnaires",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CandidateVacancy",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "integer", nullable: false),
                    VacancyId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateVacancy", x => new { x.CandidateId, x.VacancyId });
                    table.ForeignKey(
                        name: "FK_CandidateVacancy_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateVacancy_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Source = table.Column<string>(type: "text", nullable: true),
                    FileType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    QuestionnaireId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionCategories_Questionnaires_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    QuestionCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionCategories_QuestionCategoryId",
                        column: x => x.QuestionCategoryId,
                        principalTable: "QuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CandidateId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Estimation = table.Column<byte>(type: "smallint", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3cb2f618-d4f8-47c4-b92a-cd6cd1d2a850");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PhotoId",
                table: "Candidates",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_CandidateId",
                table: "Questionnaires",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_DocumentFileId",
                table: "Questionnaires",
                column: "DocumentFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CandidateId",
                table: "Answers",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateVacancy_VacancyId",
                table: "CandidateVacancy",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCategories_QuestionnaireId",
                table: "QuestionCategories",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionCategoryId",
                table: "Questions",
                column: "QuestionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Files_PhotoId",
                table: "Candidates",
                column: "PhotoId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaires_Candidates_CandidateId",
                table: "Questionnaires",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaires_Files_DocumentFileId",
                table: "Questionnaires",
                column: "DocumentFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaires_Vacancies_VacancyId",
                table: "Questionnaires",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Files_PhotoId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaires_Candidates_CandidateId",
                table: "Questionnaires");

            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaires_Files_DocumentFileId",
                table: "Questionnaires");

            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaires_Vacancies_VacancyId",
                table: "Questionnaires");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "CandidateVacancy");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuestionCategories");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_PhotoId",
                table: "Candidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questionnaires",
                table: "Questionnaires");

            migrationBuilder.DropIndex(
                name: "IX_Questionnaires_CandidateId",
                table: "Questionnaires");

            migrationBuilder.DropIndex(
                name: "IX_Questionnaires_DocumentFileId",
                table: "Questionnaires");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Questionnaires");

            migrationBuilder.DropColumn(
                name: "DocumentFileId",
                table: "Questionnaires");

            migrationBuilder.RenameTable(
                name: "Questionnaires",
                newName: "Questionnaire");

            migrationBuilder.RenameColumn(
                name: "PhotoId",
                table: "Candidates",
                newName: "VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_Questionnaires_VacancyId",
                table: "Questionnaire",
                newName: "IX_Questionnaire_VacancyId");

            migrationBuilder.AddColumn<int>(
                name: "QuestionnaireId",
                table: "Candidates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VacancyId",
                table: "Questionnaire",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questionnaire",
                table: "Questionnaire",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "23cfdb13-72f9-460b-a4d8-4ac2aea8d00a");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_QuestionnaireId",
                table: "Candidates",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_VacancyId",
                table: "Candidates",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Questionnaire_QuestionnaireId",
                table: "Candidates",
                column: "QuestionnaireId",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Vacancies_VacancyId",
                table: "Candidates",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaire_Vacancies_VacancyId",
                table: "Questionnaire",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

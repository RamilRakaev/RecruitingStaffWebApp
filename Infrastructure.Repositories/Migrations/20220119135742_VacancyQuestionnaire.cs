using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RecruitingStaff.Infrastructure.Repositories.Migrations
{
    public partial class VacancyQuestionnaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaire_Vacancy_VacancyId",
                table: "Questionnaire");

            migrationBuilder.DropIndex(
                name: "IX_Questionnaire_VacancyId",
                table: "Questionnaire");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "Questionnaire");

            migrationBuilder.CreateTable(
                name: "VacancyQuestionnaire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionnaireId = table.Column<int>(type: "integer", nullable: true),
                    FirstEntityId = table.Column<int>(type: "integer", nullable: false),
                    SecondEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyQuestionnaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacancyQuestionnaire_Questionnaire_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "Questionnaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VacancyQuestionnaire_Vacancy_FirstEntityId",
                        column: x => x.FirstEntityId,
                        principalTable: "Vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7f34c9ee-6d89-4a7e-99a2-7cb48c61c0a2");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyQuestionnaire_FirstEntityId",
                table: "VacancyQuestionnaire",
                column: "FirstEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyQuestionnaire_QuestionnaireId",
                table: "VacancyQuestionnaire",
                column: "QuestionnaireId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacancyQuestionnaire");

            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "Questionnaire",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c9910eba-8d4c-4823-bc9a-b77f5b27969d");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_VacancyId",
                table: "Questionnaire",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaire_Vacancy_VacancyId",
                table: "Questionnaire",
                column: "VacancyId",
                principalTable: "Vacancy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Repositories.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "Candidates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionnaireId",
                table: "Candidates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TelephoneNumber",
                table: "Candidates",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Questionnaire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    VacancyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionnaire_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Questionnaire_VacancyId",
                table: "Questionnaire",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Questionnaire_QuestionnaireId",
                table: "Candidates",
                column: "QuestionnaireId",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Questionnaire_QuestionnaireId",
                table: "Candidates");

            migrationBuilder.DropTable(
                name: "Questionnaire");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_QuestionnaireId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "QuestionnaireId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "TelephoneNumber",
                table: "Candidates");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "60c65a6c-9010-412c-9d45-b97ce4bf53c3");
        }
    }
}

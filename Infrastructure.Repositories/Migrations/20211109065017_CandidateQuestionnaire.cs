using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class CandidateQuestionnaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaires_Candidates_CandidateId",
                table: "Questionnaires");

            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaires_Files_DocumentFileId",
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

            migrationBuilder.CreateTable(
                name: "CandidateQuestionnaire",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "integer", nullable: false),
                    QuestionnaireId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateQuestionnaire", x => new { x.CandidateId, x.QuestionnaireId });
                    table.ForeignKey(
                        name: "FK_CandidateQuestionnaire_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateQuestionnaire_Questionnaires_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c3a087de-fbd6-4507-904a-6e17674a8fee");

            migrationBuilder.CreateIndex(
                name: "IX_Files_QuestionnaireId",
                table: "Files",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateQuestionnaire_QuestionnaireId",
                table: "CandidateQuestionnaire",
                column: "QuestionnaireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Questionnaires_QuestionnaireId",
                table: "Files",
                column: "QuestionnaireId",
                principalTable: "Questionnaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Questionnaires_QuestionnaireId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "CandidateQuestionnaire");

            migrationBuilder.DropIndex(
                name: "IX_Files_QuestionnaireId",
                table: "Files");

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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "98a6b9b1-8074-43d7-9c81-7787cc9c03ff");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_CandidateId",
                table: "Questionnaires",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_DocumentFileId",
                table: "Questionnaires",
                column: "DocumentFileId",
                unique: true);

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
        }
    }
}

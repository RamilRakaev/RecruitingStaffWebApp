using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Repositories.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Files_PhotoId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_PhotoId",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionnaireId",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhotoId1",
                table: "Candidates",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "98a6b9b1-8074-43d7-9c81-7787cc9c03ff");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CandidateId",
                table: "Files",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PhotoId1",
                table: "Candidates",
                column: "PhotoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Files_PhotoId1",
                table: "Candidates",
                column: "PhotoId1",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Candidates_CandidateId",
                table: "Files",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Files_PhotoId1",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Candidates_CandidateId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CandidateId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_PhotoId1",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "QuestionnaireId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "PhotoId1",
                table: "Candidates");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3b07a694-3dbe-4df5-a209-6c84e6f413a8");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PhotoId",
                table: "Candidates",
                column: "PhotoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Files_PhotoId",
                table: "Candidates",
                column: "PhotoId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

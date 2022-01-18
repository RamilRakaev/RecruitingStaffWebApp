using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitingStaff.Infrastructure.Repositories.Migrations
{
    public partial class TheEntitiesCandidateAndRecommenderAreRelated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Recommender",
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
                name: "IX_Recommender_CandidateId",
                table: "Recommender",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommender_Candidate_CandidateId",
                table: "Recommender",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommender_Candidate_CandidateId",
                table: "Recommender");

            migrationBuilder.DropIndex(
                name: "IX_Recommender_CandidateId",
                table: "Recommender");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Recommender");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f34628c2-9d5d-4a4c-985a-40245409b429");
        }
    }
}

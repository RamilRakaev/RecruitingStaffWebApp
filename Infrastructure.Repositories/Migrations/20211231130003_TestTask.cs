using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Repositories.Migrations
{
    public partial class TestTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestTaskId",
                table: "RecruitingStaffWebAppFile",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VacancyId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTask_Vacancy_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateTestTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestTaskId = table.Column<int>(type: "integer", nullable: true),
                    FirstEntityId = table.Column<int>(type: "integer", nullable: false),
                    SecondEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateTestTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateTestTask_Candidate_FirstEntityId",
                        column: x => x.FirstEntityId,
                        principalTable: "Candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateTestTask_TestTask_TestTaskId",
                        column: x => x.TestTaskId,
                        principalTable: "TestTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b896ecf6-dc27-42f3-acdc-dc8afc808294");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitingStaffWebAppFile_TestTaskId",
                table: "RecruitingStaffWebAppFile",
                column: "TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateTestTask_FirstEntityId",
                table: "CandidateTestTask",
                column: "FirstEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateTestTask_TestTaskId",
                table: "CandidateTestTask",
                column: "TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_VacancyId",
                table: "TestTask",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitingStaffWebAppFile_TestTask_TestTaskId",
                table: "RecruitingStaffWebAppFile",
                column: "TestTaskId",
                principalTable: "TestTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitingStaffWebAppFile_TestTask_TestTaskId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.DropTable(
                name: "CandidateTestTask");

            migrationBuilder.DropTable(
                name: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_RecruitingStaffWebAppFile_TestTaskId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.DropColumn(
                name: "TestTaskId",
                table: "RecruitingStaffWebAppFile");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d508e513-694b-4ac9-bee0-0112f9792e31");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Repositories.Migrations
{
    public partial class NewEntitiesForCandidateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Candidate",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EducationalInstitutionName = table.Column<string>(type: "text", nullable: true),
                    Specification = table.Column<string>(type: "text", nullable: true),
                    Qualification = table.Column<string>(type: "text", nullable: true),
                    CandidateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Education_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreviousJob",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationName = table.Column<string>(type: "text", nullable: true),
                    OrganizationPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    OrganizationAddress = table.Column<string>(type: "text", nullable: true),
                    PositionAtWork = table.Column<string>(type: "text", nullable: true),
                    DateOfStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Salary = table.Column<string>(type: "text", nullable: true),
                    Responsibilities = table.Column<string>(type: "text", nullable: true),
                    LeavingReason = table.Column<string>(type: "text", nullable: true),
                    CandidateId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Recommender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    PositionAtWork = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PreviousJobId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommender", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommender_PreviousJob_PreviousJobId",
                        column: x => x.PreviousJobId,
                        principalTable: "PreviousJob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ce8f81f7-4fe0-4a4f-afc6-70ee08683ab2");

            migrationBuilder.CreateIndex(
                name: "IX_Education_CandidateId",
                table: "Education",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_PreviousJob_CandidateId",
                table: "PreviousJob",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommender_PreviousJobId",
                table: "Recommender",
                column: "PreviousJobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Recommender");

            migrationBuilder.DropTable(
                name: "PreviousJob");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Candidate");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dce1f240-5d32-4980-8b17-bea2eae6bfe0");
        }
    }
}

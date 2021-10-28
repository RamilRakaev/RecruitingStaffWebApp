using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Repositories.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Contenders_ContenderId",
                table: "Options");

            migrationBuilder.DropTable(
                name: "Contenders");

            migrationBuilder.RenameColumn(
                name: "ContenderId",
                table: "Options",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_ContenderId",
                table: "Options",
                newName: "IX_Options_CandidateId");

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a0bbde55-211e-4170-9a94-813e53775251");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Candidate_CandidateId",
                table: "Options",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Candidate_CandidateId",
                table: "Options");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "Options",
                newName: "ContenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_CandidateId",
                table: "Options",
                newName: "IX_Options_ContenderId");

            migrationBuilder.CreateTable(
                name: "Contenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contenders", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "da9e1195-84e1-429c-bf5d-3bd452d77a46");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Contenders_ContenderId",
                table: "Options",
                column: "ContenderId",
                principalTable: "Contenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

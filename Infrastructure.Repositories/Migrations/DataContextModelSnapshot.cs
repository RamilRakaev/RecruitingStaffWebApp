﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RecruitingStaff.Infrastructure.Repositories;

namespace RecruitingStaff.Infrastructure.Repositories.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<byte>("Estimation")
                        .HasColumnType("smallint");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("text");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("integer");

                    b.Property<int?>("PhotoId1")
                        .HasColumnType("integer");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId1");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateQuestionnaire", b =>
                {
                    b.Property<int>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionnaireId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("CandidateId", "QuestionnaireId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("CandidateQuestionnaire");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateVacancy", b =>
                {
                    b.Property<int>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<int>("VacancyId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("CandidateId", "VacancyId");

                    b.HasIndex("VacancyId");

                    b.ToTable("CandidateVacancy");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("QuestionCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuestionCategoryId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.QuestionCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("QuestionnaireId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("QuestionCategory");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Questionnaire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("VacancyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VacancyId");

                    b.ToTable("Questionnaire");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.RecruitingStaffWebAppFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<int>("FileType")
                        .HasColumnType("integer");

                    b.Property<int?>("QuestionnaireId")
                        .HasColumnType("integer");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("RecruitingStaffWebAppFile");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Requirements")
                        .HasColumnType("text");

                    b.Property<string>("Responsibilities")
                        .HasColumnType("text");

                    b.Property<string>("WorkingConditions")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vacancy");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<string>("PropertyName")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("Option");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "6065829b-eaf6-4614-8dea-ff0e69938aed",
                            Name = "user",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<int?>("ApplicationRoleId")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationRoleId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Answer", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Candidate", "Candidate")
                        .WithMany("Answers")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Candidate", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.RecruitingStaffWebAppFile", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId1");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateQuestionnaire", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Candidate", "Candidate")
                        .WithMany("CandidateQuestionnaires")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Questionnaire", "Questionnaire")
                        .WithMany("CandidateQuestionnaires")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateVacancy", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Candidate", "Candidate")
                        .WithMany("CandidateVacancies")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Vacancy", "Vacancy")
                        .WithMany("CandidateVacancies")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Question", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.QuestionCategory", "QuestionCategory")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionCategory");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.QuestionCategory", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Questionnaire", "Questionnaire")
                        .WithMany("QuestionCategories")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Questionnaire", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Vacancy", "Vacancy")
                        .WithMany("Questionnaires")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.RecruitingStaffWebAppFile", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Candidate", "Candidate")
                        .WithMany("Documents")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Questionnaire", "Questionnaire")
                        .WithMany("DocumentFiles")
                        .HasForeignKey("QuestionnaireId");

                    b.Navigation("Candidate");

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.Option", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Candidate", "Candidate")
                        .WithMany("Options")
                        .HasForeignKey("CandidateId");

                    b.Navigation("Candidate");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationRoleClaim", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.UserIdentity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUser", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.UserIdentity.ApplicationRole", null)
                        .WithMany("Users")
                        .HasForeignKey("ApplicationRoleId");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUserClaim", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUserLogin", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUserRole", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.UserIdentity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUserToken", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.UserIdentity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Candidate", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("CandidateQuestionnaires");

                    b.Navigation("CandidateVacancies");

                    b.Navigation("Documents");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.QuestionCategory", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Questionnaire", b =>
                {
                    b.Navigation("CandidateQuestionnaires");

                    b.Navigation("DocumentFiles");

                    b.Navigation("QuestionCategories");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidateQuestionnaire.Vacancy", b =>
                {
                    b.Navigation("CandidateVacancies");

                    b.Navigation("Questionnaires");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

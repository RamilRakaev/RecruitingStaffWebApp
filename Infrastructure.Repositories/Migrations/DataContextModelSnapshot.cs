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

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Answer", b =>
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

                    b.Property<string>("FamiliarWithTheTechnology")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("AddressIndex")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("text");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("integer");

                    b.Property<int?>("PhotoId1")
                        .HasColumnType("integer");

                    b.Property<string>("PlaceOfBirth")
                        .HasColumnType("text");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId1");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDateOfTraining")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Qualification")
                        .HasColumnType("text");

                    b.Property<string>("Specialization")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDateOfTraining")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Kid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("Kid");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("Option");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.PreviousJobPlacement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateOfStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LeavingReason")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("PositionAtWork")
                        .HasColumnType("text");

                    b.Property<string>("Responsibilities")
                        .HasColumnType("text");

                    b.Property<string>("Salary")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("PreviousJobPlacement");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Recommender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("PositionAtWork")
                        .HasColumnType("text");

                    b.Property<int>("PreviousJobId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PreviousJobId");

                    b.ToTable("Recommender");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Maps.CandidateQuestionnaire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FirstEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("SecondEntityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FirstEntityId");

                    b.HasIndex("SecondEntityId");

                    b.ToTable("CandidateQuestionnaire");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Maps.CandidateTestTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FirstEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("SecondEntityId")
                        .HasColumnType("integer");

                    b.Property<int?>("TestTaskId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FirstEntityId");

                    b.HasIndex("TestTaskId");

                    b.ToTable("CandidateTestTask");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Maps.CandidateVacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CandidateStatus")
                        .HasColumnType("integer");

                    b.Property<int>("FirstEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("SecondEntityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FirstEntityId");

                    b.HasIndex("SecondEntityId");

                    b.ToTable("CandidateVacancy");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Question", b =>
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

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.QuestionCategory", b =>
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

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Questionnaire", b =>
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

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.RecruitingStaffWebAppFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CandidateId")
                        .HasColumnType("integer");

                    b.Property<int>("FileType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("QuestionnaireId")
                        .HasColumnType("integer");

                    b.Property<int?>("TestTaskId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("QuestionnaireId");

                    b.HasIndex("TestTaskId")
                        .IsUnique();

                    b.ToTable("RecruitingStaffWebAppFile");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.TestTask", b =>
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

                    b.ToTable("TestTask");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Vacancy", b =>
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
                            ConcurrencyStamp = "f34628c2-9d5d-4a4c-985a-40245409b429",
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

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Answer", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", "Candidate")
                        .WithMany("Answers")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.RecruitingStaffWebAppFile", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId1");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Education", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", "Candidate")
                        .WithMany("Educations")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Kid", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", "Candidate")
                        .WithMany("Kids")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Option", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", "Candidate")
                        .WithMany("Options")
                        .HasForeignKey("CandidateId");

                    b.Navigation("Candidate");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.PreviousJobPlacement", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", "Candidate")
                        .WithMany("PreviousJobs")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Recommender", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.PreviousJobPlacement", "PreviousJob")
                        .WithMany("Recommenders")
                        .HasForeignKey("PreviousJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PreviousJob");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Maps.CandidateQuestionnaire", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", "Candidate")
                        .WithMany("CandidateQuestionnaire")
                        .HasForeignKey("FirstEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.Questionnaire", "Questionnaire")
                        .WithMany("CandidatesQuestionnaire")
                        .HasForeignKey("SecondEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Maps.CandidateTestTask", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", "Candidate")
                        .WithMany("CandidateTestTasks")
                        .HasForeignKey("FirstEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.TestTask", "TestTask")
                        .WithMany("CandidateTestTasks")
                        .HasForeignKey("TestTaskId");

                    b.Navigation("Candidate");

                    b.Navigation("TestTask");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Maps.CandidateVacancy", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", "Candidate")
                        .WithMany("CandidateVacancy")
                        .HasForeignKey("FirstEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.Vacancy", "Vacancy")
                        .WithMany("CandidateVacancy")
                        .HasForeignKey("SecondEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Question", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.QuestionCategory", "QuestionCategory")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionCategory");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.QuestionCategory", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.Questionnaire", "Questionnaire")
                        .WithMany("QuestionCategories")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Questionnaire", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.Vacancy", "Vacancy")
                        .WithMany("Questionnaires")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.RecruitingStaffWebAppFile", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", "Candidate")
                        .WithMany("Documents")
                        .HasForeignKey("CandidateId");

                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.Questionnaire", "Questionnaire")
                        .WithMany("DocumentFiles")
                        .HasForeignKey("QuestionnaireId");

                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.TestTask", "TestTask")
                        .WithOne("DocumentFile")
                        .HasForeignKey("RecruitingStaff.Domain.Model.CandidatesSelection.RecruitingStaffWebAppFile", "TestTaskId");

                    b.Navigation("Candidate");

                    b.Navigation("Questionnaire");

                    b.Navigation("TestTask");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.TestTask", b =>
                {
                    b.HasOne("RecruitingStaff.Domain.Model.CandidatesSelection.Vacancy", "Vacancy")
                        .WithMany("TestTasks")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vacancy");
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

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.Candidate", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("CandidateQuestionnaire");

                    b.Navigation("CandidateTestTasks");

                    b.Navigation("CandidateVacancy");

                    b.Navigation("Documents");

                    b.Navigation("Educations");

                    b.Navigation("Kids");

                    b.Navigation("Options");

                    b.Navigation("PreviousJobs");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData.PreviousJobPlacement", b =>
                {
                    b.Navigation("Recommenders");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.QuestionCategory", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Questionnaire", b =>
                {
                    b.Navigation("CandidatesQuestionnaire");

                    b.Navigation("DocumentFiles");

                    b.Navigation("QuestionCategories");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.TestTask", b =>
                {
                    b.Navigation("CandidateTestTasks");

                    b.Navigation("DocumentFile");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.CandidatesSelection.Vacancy", b =>
                {
                    b.Navigation("CandidateVacancy");

                    b.Navigation("Questionnaires");

                    b.Navigation("TestTasks");
                });

            modelBuilder.Entity("RecruitingStaff.Domain.Model.UserIdentity.ApplicationRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

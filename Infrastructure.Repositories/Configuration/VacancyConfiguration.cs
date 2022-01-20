using RecruitingStaff.Domain.Model.CandidatesSelection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder
                .HasMany(v => v.CandidateVacancy)
                .WithOne(cv => cv.Vacancy)
                .HasForeignKey(cv => cv.SecondEntityId);

            builder
                .HasMany(v => v.VacancyQuestionnaire)
                .WithOne(cv => cv.Vacancy)
                .HasForeignKey(cv => cv.FirstEntityId);

            builder
                .HasMany(v => v.TestTasks)
                .WithOne(tt => tt.Vacancy)
                .HasForeignKey(tt => tt.VacancyId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class CandidateVacancyConfiguration : IEntityTypeConfiguration<CandidateVacancy>
    {
        public void Configure(EntityTypeBuilder<CandidateVacancy> builder)
        {
            builder.HasOne(c => c.Candidate).WithMany(c => c.CandidateVacancies).HasForeignKey(c => c.CandidateId);
            builder.HasOne(v => v.Vacancy).WithMany(c => c.CandidateVacancies).HasForeignKey(v => v.VacancyId);
        }
    }
}

using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.HasMany(v => v.Candidates)
                .WithOne(c => c.VacancyClaim)
                .IsRequired(false)
                .HasForeignKey(c => c.VacancyId)
                .IsRequired(false);
        }
    }
}
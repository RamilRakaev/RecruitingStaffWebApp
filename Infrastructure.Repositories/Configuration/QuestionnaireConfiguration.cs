using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
    {
        public void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
            builder.HasOne(q => q.Vacancy).WithOne(v => v.Questionnaire).HasForeignKey<Vacancy>(q => q.QuestionnaireId);
        }
    }
}
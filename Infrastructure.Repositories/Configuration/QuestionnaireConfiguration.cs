using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
    {
        public void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
            builder
                .HasMany(q => q.CandidatesQuestionnaire)
                .WithOne(cq => cq.Questionnaire)
                .HasForeignKey(cq => cq.SecondEntityId);
        }
    }
}

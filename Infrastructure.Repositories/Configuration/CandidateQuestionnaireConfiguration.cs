using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class CandidateQuestionnaireConfiguration : IEntityTypeConfiguration<CandidateQuestionnaire>
    {
        public void Configure(EntityTypeBuilder<CandidateQuestionnaire> builder)
        {
            builder
                .HasKey(cq => new { cq.CandidateId, cq.QuestionnaireId });
            builder
                .HasOne(c => c.Candidate)
                .WithMany(c => c.CandidateQuestionnaires)
                .HasForeignKey(cq => cq.CandidateId);
            builder
                .HasOne(q => q.Questionnaire)
                .WithMany(q => q.CandidateQuestionnaires)
                .HasForeignKey(cq => cq.QuestionnaireId);
        }
    }
}

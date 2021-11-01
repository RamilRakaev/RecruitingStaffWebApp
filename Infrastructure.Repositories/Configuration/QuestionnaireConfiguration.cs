using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
    {
        public void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
            builder.HasOne(q => q.Candidate).WithMany(c => c.Questionnaires).HasForeignKey(q => q.CandidateId);
            builder.HasOne(q => q.DocumentFile).WithOne(d => d.Questionnaire).HasForeignKey<Questionnaire>(q => q.DocumentFileId);
        }
    }
}
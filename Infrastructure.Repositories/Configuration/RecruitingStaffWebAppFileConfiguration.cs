using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class RecruitingStaffWebAppFileConfiguration : IEntityTypeConfiguration<RecruitingStaffWebAppFile>
    {
        public void Configure(EntityTypeBuilder<RecruitingStaffWebAppFile> builder)
        {
            builder
                .HasOne(f => f.Candidate)
                .WithMany(c => c.Documents)
                .HasForeignKey(f => f.CandidateId);

            builder
                .HasOne(f => f.Questionnaire)
                .WithMany(q => q.DocumentFiles)
                .HasForeignKey(f => f.QuestionnaireId);

            builder
                .HasOne(f => f.TestTask)
                .WithOne(q => q.DocumentFile)
                .HasForeignKey<RecruitingStaffWebAppFile>(f => f.TestTaskId);
        }
    }
}

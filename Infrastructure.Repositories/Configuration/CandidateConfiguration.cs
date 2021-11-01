using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasMany(c => c.Options)
                .WithOne(o => o.Candidate)
                .IsRequired(false)
                .HasForeignKey(o => o.CandidateId)
                .IsRequired(false);
        }
    }
}

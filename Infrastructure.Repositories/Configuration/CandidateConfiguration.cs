using Domain.Model.CandidateQuestionnaire;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repositories.Configuration
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

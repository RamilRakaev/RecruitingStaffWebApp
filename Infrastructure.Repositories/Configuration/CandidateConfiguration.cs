using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder
                .HasMany(c => c.PreviousJobs)
                .WithOne(pj => pj.Candidate)
                .HasForeignKey(pj => pj.CandidateId);

            builder
                .HasMany(c => c.Educations)
                .WithOne(e => e.Candidate)
                .HasForeignKey(e => e.CandidateId);

            builder
                .HasMany(c => c.Kids)
                .WithOne(e => e.Candidate)
                .HasForeignKey(e => e.CandidateId);

            builder
                .HasMany(c => c.CandidateQuestionnaires)
                .WithMany(q => q.CandidateQuestionnaires);

            builder
                .HasMany(c => c.CandidateVacancies)
                .WithMany(q => q.CandidateVacancies);
        }
    }
}

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
                .HasMany(c => c.CandidateQuestionnaire)
                .WithOne(cq => cq.Candidate)
                .HasForeignKey(c => c.CandidateId);

            builder
                .HasMany(c => c.CandidateVacancy)
                .WithOne(cv => cv.Candidate)
                .HasForeignKey(cv => cv.CandidateId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasOne(o => o.Candidate).WithMany(c => c.Options).HasForeignKey(o => o.CandidateId);
        }
    }
}

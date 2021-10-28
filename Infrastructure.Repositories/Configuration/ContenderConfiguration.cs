using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repositories.Configuration
{
    public class ContenderConfiguration : IEntityTypeConfiguration<Contender>
    {
        public void Configure(EntityTypeBuilder<Contender> builder)
        {
            builder.HasMany(c => c.Options)
                .WithOne(o => o.Contender)
                .IsRequired(false)
                .HasForeignKey(o => o.ContenderId)
                .IsRequired(false);
        }
    }
}

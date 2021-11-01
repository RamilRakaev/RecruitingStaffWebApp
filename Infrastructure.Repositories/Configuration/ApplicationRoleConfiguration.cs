using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitingStaff.Domain.Model.UserIdentity;

namespace RecruitingStaff.Infrastructure.Repositories.Configuration
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(new ApplicationRole[]
            {
                new ApplicationRole(){ Id = 1, Name = "user", NormalizedName = "USER"}
            });
        }
    }
}

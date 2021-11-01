using RecruitingStaff.Infrastructure.Repositories.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.Repositories
{
    public class DataContext : 
        IdentityDbContext<ApplicationUser,
            ApplicationRole,
            int,
            ApplicationUserClaim,
            ApplicationUserRole,
            ApplicationUserLogin,
            ApplicationRoleClaim,
            ApplicationUserToken>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new CandidateConfiguration());
            builder.ApplyConfiguration(new VacancyConfiguration());
            base.OnModelCreating(builder);
        }
    }
}

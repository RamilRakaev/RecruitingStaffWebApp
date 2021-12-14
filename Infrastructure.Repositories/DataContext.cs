using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Infrastructure.Repositories.Configuration;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnswerConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new RecruitingStaffWebAppFileConfiguration());
            builder.ApplyConfiguration(new CandidateConfiguration());
            //builder.ApplyConfiguration(new CandidateVacancyConfiguration());
            builder.ApplyConfiguration(new OptionConfiguration());
            builder.ApplyConfiguration(new QuestionCategoryConfiguration());
            builder.ApplyConfiguration(new QuestionConfiguration());
            //builder.ApplyConfiguration(new CandidateQuestionnaireConfiguration());
            builder.ApplyConfiguration(new VacancyConfiguration());
            base.OnModelCreating(builder);
        }
    }
}

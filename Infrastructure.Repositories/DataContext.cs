using Domain.Model;
using Domain.Model.UserIdentity;
using Infrastructure.Repositories.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
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

        public DbSet<Contender> Contenders { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            base.OnModelCreating(builder);
        }
    }
}

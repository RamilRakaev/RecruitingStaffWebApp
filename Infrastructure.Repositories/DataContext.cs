﻿using Domain.Model;
using Domain.Model.UserIdentity;
using Infrastructure.Repositories.Configuration;
using Microsoft.AspNetCore.Identity;
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
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole[]
            {
                new ApplicationRole(){ Id = 1, Name = "user", NormalizedName = "USER"}
            });

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}

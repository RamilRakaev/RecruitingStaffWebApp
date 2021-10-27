using Domain.Interfaces;
using Domain.Model;
using Infrastructure.Repositories;
using Infrastructure.Repositories.SubRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Domain.Validators;
using FluentValidation.AspNetCore;
using CQRS;
using MediatR;
using Infrastructure.CQRS;
using RecruitingStaffWebApp.Pages.Account;
using Infrastructure.Services.Repositories;
using Domain.Model.UserIdentity;

namespace RecruitingStaffWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(o => o.UseNpgsql(Configuration.GetConnectionString("DefaultDbConnection"),
                o => o.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<DataContext>();

            services.AddTransient<IRepository<Contender>, ContenderRepository>();
            services.AddTransient<IRepository<Option>, OptionRepository>();

            services.AddHostedService<MigrationService>();
            services.AddHostedService<UserService>();

            services.AddMediatR(CQRSAssemblyInfo.Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(CQRSAssemblyInfo.Assembly);
            services.AddTransient<IValidator<ApplicationUser>, ApplicationUserValidator>();
            services.AddTransient<IValidator<Contender>, ContenderValidator>();

            services.AddRazorPages().AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

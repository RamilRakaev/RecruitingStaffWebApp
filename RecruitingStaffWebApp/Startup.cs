using RecruitingStaff.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using RecruitingStaff.Infrastructure.CQRS;
using RecruitingStaff.Infrastructure.DatabaseServices;
using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Validators;
using RecruitingStaffWebApp.Services.DocParse;
using RecruitingStaffWebApp.Infrastructure.DocParse;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

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

            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddHostedService<MigrationService>();
            services.AddHostedService<UserService>();

            services.Configure<WebAppOptions>(Configuration.GetSection("WebAppOptions"));

            services.AddTransient<IQuestionnaireManager, QuestionnaireManager>();

            services.AddMediatR(CQRSAssemblyInfo.Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(CQRSAssemblyInfo.Assembly);

            services.AddTransient<IValidator<ApplicationUser>, ApplicationUserValidator>();
            services.AddTransient<IValidator<Candidate>, CandidateValidator>();
            services.AddTransient<IValidator<Vacancy>, VacancyValidator>();
            services.AddTransient<IValidator<Questionnaire>, QuestionnaireValidator>();

            services.AddRazorPages().AddFluentValidation();
            services.AddAuthorization(option =>
            {
                option.AddPolicy("RequireUserRole",
                    policy => policy.RequireRole("user"));
            });
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

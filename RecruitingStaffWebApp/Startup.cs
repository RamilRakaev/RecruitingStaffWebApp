using RecruitingStaff.Infrastructure.Repositories;
using RecruitingStaff.Infrastructure.Repositories.SubRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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

            services.AddTransient<IRepository<Vacancy>, VacancyRepository>();
            services.AddTransient<IRepository<Candidate>, CandidateRepository>();
            services.AddTransient<IRepository<RecruitingStaffWebAppFile>, RecruitingStaffWebAppFileRepository>();
            services.AddTransient<IRepository<Questionnaire>, QuestionnaireRepository>();
            services.AddTransient<IRepository<Answer>, AnswerRepository>();
            services.AddTransient<IRepository<QuestionCategory>, QuestionCategoryRepository>();
            services.AddTransient<IRepository<Question>, QuestionRepository>();
            services.AddTransient<IRepository<Option>, OptionRepository>();

            services.AddHostedService<MigrationService>();
            services.AddHostedService<UserService>();

            services.Configure<WebAppOptions>(Configuration.GetSection("WebAppOptions"));

            services.AddMediatR(CQRSAssemblyInfo.Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(CQRSAssemblyInfo.Assembly);

            services.AddTransient<IValidator<ApplicationUser>, ApplicationUserValidator>();
            services.AddTransient<IValidator<Candidate>, CandidateValidator>();
            services.AddTransient<IValidator<Vacancy>, VacancyValidator>();
            services.AddTransient<IValidator<Questionnaire>, QuestionnaireValidator>();

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

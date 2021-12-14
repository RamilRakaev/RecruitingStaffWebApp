using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Infrastructure.CQRS;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.DatabaseServices;
using RecruitingStaff.Infrastructure.Repositories;
using RecruitingStaffWebApp.Infrastructure.DocParse;
using RecruitingStaffWebApp.Services.DocParse;

namespace RecruitingStaff.WebApp
{
    public static class StartupExtensions
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));


            services.AddHostedService<MigrationService>();
            services.AddHostedService<UserService>();

            services.AddTransient<IQuestionnaireManager, QuestionnaireManager>();

            services.AddMediatR(CQRSAssemblyInfo.Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(CQRSAssemblyInfo.Assembly);

            services.AddTransient(typeof(ICreateOrChangeEntityHandler<>), typeof(CreateOrChangeEntityHandler<>));
            services.AddTransient(typeof(CreateOrChangeEntityCommand<>), typeof(CreateOrChangeEntityCommand<>));
            services.AddTransient(typeof(CreateOrChangeEntityHandler<>), typeof(CreateOrChangeEntityHandler<>));
            //services.AddTransient<IValidator<ApplicationUser>, ApplicationUserValidator>();
            //services.AddTransient<IValidator<Candidate>, CandidateValidator>();
            //services.AddTransient<IValidator<Vacancy>, VacancyValidator>();
            //services.AddTransient<IValidator<Questionnaire>, QuestionnaireValidator>();
        }
    }
}

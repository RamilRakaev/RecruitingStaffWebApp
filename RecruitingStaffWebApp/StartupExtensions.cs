using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Validators;
using RecruitingStaff.Infrastructure.CQRS;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.DatabaseServices;
using RecruitingStaff.Infrastructure.Repositories;
using RecruitingStaff.WebApp.ViewModels.ApplicationUser;
using RecruitingStaffWebApp.Infrastructure.DocParse;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Linq;

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

            services.AddTransient<IValidator<ApplicationUserViewModel>, ApplicationUserValidator>();
            //services.AddTransient<IValidator<Candidate>, CandidateValidator>();
            //services.AddTransient<IValidator<Vacancy>, VacancyValidator>();
            //services.AddTransient<IValidator<Questionnaire>, QuestionnaireValidator>();
            services.ConfigrueHandlers<bool>(typeof(CreateOrChangeEntityCommand<>), typeof(CreateOrChangeEntityHandler<>));
            services.ConfigrueHandlers<bool>(typeof(CreateOrChangeEntityByKeysCommand<>), typeof(CreateOrChangeEntityByKeysHandler<>));
            services.ConfigrueHandlers<bool>(typeof(ChangeEntityCommand<>), typeof(ChangeEntityHandler<>));
            services.ConfigrueHandlers(typeof(CreateEntityCommand<>), typeof(CreateEntityHandler<>));
            services.ConfigrueHandlers(typeof(GetEntityByIdQuery<>), typeof(GetEntityByIdHandler<>));
        }

        public static void ConfigrueHandlers<TResult>(this IServiceCollection services, Type commandType, Type handlerType)
        {
            var types = typeof(BaseEntity)
                .Assembly
                .GetTypes()
                .Where(t => t.BaseType.Equals(typeof(BaseEntity)));
            foreach (var entityType in types)
            {
                var currentCommandType = commandType.MakeGenericType(entityType);
                var iRequestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(currentCommandType, typeof(TResult));
                var currentHandlerType = handlerType.MakeGenericType(entityType);

                services.AddTransient(iRequestHandlerType, currentHandlerType);
            }
        }

        public static void ConfigrueHandlers(this IServiceCollection services, Type commandType, Type handlerType)
        {
            var types = typeof(BaseEntity)
                .Assembly
                .GetTypes()
                .Where(t => t.BaseType.Equals(typeof(BaseEntity)));
            foreach (var entityType in types)
            {
                var currentCommandType = commandType.MakeGenericType(entityType);
                var iRequestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(currentCommandType, entityType);
                var currentHandlerType = handlerType.MakeGenericType(entityType);

                services.AddTransient(iRequestHandlerType, currentHandlerType);
            }
        }
    }
}

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.DatabaseServices;
using RecruitingStaff.Infrastructure.Repositories;
using RecruitingStaffWebApp.Infrastructure.DocParse;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Linq;
using System.Reflection;

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

            services.ConfigrueHandlers<CandidateQuestionnaireEntity>(typeof(CreateOrChangeEntityCommand<>), typeof(CreateOrChangeEntityHandler<>));
            services.ConfigrueHandlers<CandidateQuestionnaireEntity>(typeof(CreateOrChangeEntityByKeysCommand<>), typeof(CreateOrChangeEntityByKeysHandler<>));
            services.ConfigrueHandlers<CandidateQuestionnaireEntity>(typeof(ChangeEntityCommand<>), typeof(ChangeEntityHandler<>));
            services.ConfigrueHandlers<CandidateQuestionnaireEntity>(typeof(CreateEntityCommand<>), typeof(CreateEntityHandler<>));
            services.ConfigrueHandlers<CandidateQuestionnaireEntity>(typeof(RemoveEntityCommand<>), typeof(RemoveEntityHandler<>));
            services.ConfigrueHandlers<BaseMap>(typeof(RemoveEntityCommand<>), typeof(RemoveEntityHandler<>));
            services.ConfigrueHandlers<CandidateQuestionnaireEntity>(typeof(GetEntityByIdQuery<>), typeof(GetEntityByIdHandler<>));
            services.ConfigrueEntitiesQueryHandlers<CandidateQuestionnaireEntity>(typeof(GetEntitiesQuery<>), typeof(GetEntitiesHandler<>));
            services.ConfigrueEntitiesQueryHandlers<CandidateQuestionnaireEntity>(typeof(GetEntitiesByForeignKeyQuery<>), typeof(GetEntitiesByForeignKeyHandler<>));
            services.ConfigrueEntitiesQueryHandlers<BaseMap>(typeof(GetEntitiesByForeignKeyQuery<>), typeof(GetEntitiesByForeignKeyHandler<>));
            services.ConfigrueHandlers<BaseMap>(typeof(CreateMapCommand<>), typeof(CreateMapHandler<>));

            services.ConfigureViewModelValidators();
        }

        public static void ConfigrueEntitiesQueryHandlers<Entity>(this IServiceCollection services, Type commandType, Type handlerType)
        {
            var types = typeof(BaseEntity)
                .Assembly
                .GetTypes()
                .Where(t => t.BaseType.Equals(typeof(Entity)));
            foreach (var entityType in types)
            {
                var arrayType = Array.CreateInstance(entityType, 0).GetType();
                
                var currentCommandType = commandType.MakeGenericType(entityType);
                var iRequestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(currentCommandType, arrayType);
                var currentHandlerType = handlerType.MakeGenericType(entityType);

                services.AddTransient(iRequestHandlerType, currentHandlerType);
            }
        }

        public static void ConfigrueHandlers<Entity>(this IServiceCollection services, Type commandType, Type handlerType)
        {
            var types = typeof(BaseEntity)
                .Assembly
                .GetTypes()
                .Where(t => t.BaseType.Equals(typeof(Entity)));
            foreach (var entityType in types)
            {
                var currentCommandType = commandType.MakeGenericType(entityType);
                var iRequestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(currentCommandType, entityType);
                var currentHandlerType = handlerType.MakeGenericType(entityType);

                services.AddTransient(iRequestHandlerType, currentHandlerType);
            }
        }

        public static void ConfigureViewModelValidators(this IServiceCollection services)
        {
            var validatorTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.Contains("Validator"));
            foreach (var validatorType in validatorTypes)
            {
                var viewModel = validatorType.BaseType.GetGenericArguments();
                var iValidatorType = typeof(IValidator<>).MakeGenericType(viewModel);
                services.AddTransient(iValidatorType, validatorType);
            }
        }
    }
}

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.BaseEntities;
using RecruitingStaff.Infrastructure.CQRS;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries.Maps;
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

            services.ConfigrueEntitiesQueryHandlers<CandidateDataEntity>(typeof(GetCandidateDataEntitiesByIdQuery<>), typeof(GetCandidateDataEntitiesByIdHandler<>));
            services.ConfigrueEntitiesQueryHandlers<CandidateDataEntity>(typeof(GetEntitiesByForeignKeyQuery<>), typeof(GetEntitiesByForeignKeyHandler<>));
            services.ConfigrueHandlers<CandidateDataEntity>(typeof(CreateEntityCommand<>), typeof(CreateEntityHandler<>));
            services.ConfigrueHandlers<CandidateDataEntity>(typeof(RemoveEntityCommand<>), typeof(RemoveEntityHandler<>));

            services.ConfigrueHandlers<BaseMap>(typeof(CreateOrChangeMapCommand<>), typeof(CreateOrChangeMapHandler<>));
            services.ConfigrueHandlers<BaseMap>(typeof(CreateMapCommand<>), typeof(CreateMapHandler<>));
            services.ConfigrueHandlers<BaseMap>(typeof(CreateEntityCommand<>), typeof(CreateEntityHandler<>));
            services.ConfigrueHandlers<BaseMap>(typeof(ChangeEntityCommand<>), typeof(ChangeEntityHandler<>));
            services.ConfigrueHandlers<BaseMap>(typeof(RemoveEntityCommand<>), typeof(RemoveEntityHandler<>)); services.ConfigrueHandlers<CandidatesSelectionEntity>(typeof(CreateOrChangeEntityCommand<>), typeof(CreateOrChangeEntityHandler<>));
            services.ConfigrueHandlers<CandidatesSelectionEntity>(typeof(CreateOrChangeEntityByKeysCommand<>), typeof(CreateOrChangeEntityByKeysHandler<>));
            services.ConfigrueHandlers<CandidatesSelectionEntity>(typeof(ChangeEntityCommand<>), typeof(ChangeEntityHandler<>));
            services.ConfigrueHandlers<CandidatesSelectionEntity>(typeof(CreateEntityCommand<>), typeof(CreateEntityHandler<>));
            services.ConfigrueHandlers<CandidatesSelectionEntity>(typeof(RemoveEntityCommand<>), typeof(RemoveEntityHandler<>));

            services.ConfigrueEntitiesQueryHandlers<BaseMap>(typeof(GetMapsByFirstEntityIdQuery<>), typeof(GetMapsByFirstEntityIdHandler<>));
            services.ConfigrueHandlers<CandidatesSelectionEntity>(typeof(GetEntityByIdQuery<>), typeof(GetEntityByIdHandler<>));
            services.ConfigrueHandlers<CandidatesSelectionEntity>(typeof(GetEntityByNameQuery<>), typeof(GetEntityByNameHandler<>));
            services.ConfigrueEntitiesQueryHandlers<CandidatesSelectionEntity>(typeof(GetEntitiesQuery<>), typeof(GetEntitiesHandler<>));
            services.ConfigrueEntitiesQueryHandlers<CandidatesSelectionEntity>(typeof(GetEntitiesByForeignKeyQuery<>), typeof(GetEntitiesByForeignKeyHandler<>));
            services.ConfigrueEntitiesQueryHandlers<BaseMap>(typeof(GetEntitiesByForeignKeyQuery<>), typeof(GetEntitiesByForeignKeyHandler<>));


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

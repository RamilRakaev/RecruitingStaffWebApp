using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.DatabaseServices;
using RecruitingStaff.Infrastructure.Repositories;
using RecruitingStaffWebApp.Infrastructure.DocParse;
using RecruitingStaffWebApp.Services.DocParse;
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
            services.ConfigrueHandlers();
        }

        public static void ConfigrueHandlers(this IServiceCollection services)
        {
            var types = typeof(BaseEntity)
                .Assembly
                .GetTypes()
                .Where(t => t.BaseType.Equals(typeof(BaseEntity)));
            foreach (var entityType in types)
            {
                var commandType = typeof(CreateOrChangeEntityCommand<>).MakeGenericType(entityType);
                var iRequestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(commandType, typeof(bool));
                var handlerType = typeof(CreateOrChangeEntityHandler<>).MakeGenericType(entityType);

                services.AddTransient(iRequestHandlerType, handlerType);
            }
        }
    }
}

using MediatR;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers
{
    public class CreateOrChangeByViewModelHandler : IRequestHandler<CreateOrChangeByViewModelCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly DataContext _context;

        public CreateOrChangeByViewModelHandler(IMediator mediator, DataContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<bool> Handle(CreateOrChangeByViewModelCommand request, CancellationToken cancellationToken)
        {
            var entityName = request.ViewModel.GetType().Name;
            entityName = entityName[..entityName.IndexOf("ViewModel")];
            var types = Assembly
                .GetAssembly(typeof(BaseEntity))
                .GetTypes()
                .Where(t => t.BaseType == typeof(BaseEntity));
            var entityType = Assembly
                .GetAssembly(typeof(BaseEntity))
                .GetTypes()
                .Where(t => t.BaseType == typeof(BaseEntity) && t.Name == entityName)
                .FirstOrDefault();
            var constructor = entityType.GetConstructor(Array.Empty<Type>());
            var entity = constructor.Invoke(Array.Empty<object>());

            foreach (var property in request.ViewModel.GetType().GetProperties())
            {
                entityType
                    .GetProperties()
                    .Where(p => p.Name == property.Name && p.PropertyType == property.PropertyType)
                    .First()
                    .SetValue(entity, property.GetValue(request.ViewModel));
            }

            var commandType = typeof(CreateOrChangeEntityCommand<>).MakeGenericType(entityType);
            var commandConstructor = commandType.GetConstructor(new Type[] { entityType });
            var command = commandConstructor.Invoke(new[] { entity });
            var r = command.GetType();
            await _mediator.Send(command, cancellationToken);
            return true;
        }
    }
}

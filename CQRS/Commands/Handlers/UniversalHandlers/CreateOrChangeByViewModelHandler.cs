using MediatR;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
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

        public CreateOrChangeByViewModelHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateOrChangeByViewModelCommand request, CancellationToken cancellationToken)
        {
            var entityName = request.ViewModel.GetType().Name;
            entityName = entityName[..entityName.IndexOf("ViewModel")];
            var types = Assembly
                .GetAssembly(typeof(BaseEntity))
                .GetTypes()
                .Where(t => t.BaseType == typeof(BaseEntity));
            var type = Assembly
                .GetAssembly(typeof(BaseEntity))
                .GetTypes()
                .Where(t => t.BaseType == typeof(BaseEntity) && t.Name == entityName)
                .FirstOrDefault();
            var constructor = type.GetConstructor(Array.Empty<Type>());
            var entity = constructor.Invoke(Array.Empty<object>());
            await _mediator.Send(new CreateOrChangeEntityCommand<BaseEntity>(entity as BaseEntity), cancellationToken);
            throw new NotImplementedException();
        }
    }
}

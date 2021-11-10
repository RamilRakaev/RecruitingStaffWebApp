using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Options
{
    public class GetOptionsHandler : IRequestHandler<GetOptionsQuery, Option[]>
    {
        private readonly IRepository<Option> _optionRepisotory;

        public GetOptionsHandler(IRepository<Option> optionRepisotory)
        {
            _optionRepisotory = optionRepisotory;
        }

        public Task<Option[]> Handle(GetOptionsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_optionRepisotory.GetAllAsNoTracking().ToArray());
        }
    }
}

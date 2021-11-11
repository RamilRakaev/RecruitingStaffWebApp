using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Options
{
    public class GetOptionsByCandidateIdHandler : IRequestHandler<GetOptionsByCandidateIdQuery, Option[]>
    {
        private readonly IRepository<Option> _optionRepository;

        public GetOptionsByCandidateIdHandler(IRepository<Option> optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public Task<Option[]> Handle(GetOptionsByCandidateIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_optionRepository.GetAllAsNoTracking().Where(o => o.CandidateId == request.CandidateId).ToArray());
        }
    }
}

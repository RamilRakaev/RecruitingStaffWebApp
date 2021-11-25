using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.PreviousJobs;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates.CandidateData.PreviousJobs
{
    public class CreatePreviousJobHandler : IRequestHandler<CreatePreviousJobCommand, PreviousJob>
    {
        private readonly IRepository<PreviousJob> _previousJobRepository;

        public CreatePreviousJobHandler(IRepository<PreviousJob> previousJobRepository)
        {
            _previousJobRepository = previousJobRepository;
        }

        public async Task<PreviousJob> Handle(CreatePreviousJobCommand request, CancellationToken cancellationToken)
        {
            await _previousJobRepository.AddAsync(request.PreviousJob, cancellationToken);
            await _previousJobRepository.SaveAsync(cancellationToken);
            return request.PreviousJob;
        }
    }
}

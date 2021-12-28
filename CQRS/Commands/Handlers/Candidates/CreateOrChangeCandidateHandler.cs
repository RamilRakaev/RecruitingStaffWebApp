using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class CreateOrChangeCandidateHandler : IRequestHandler<CreateOrChangeCandidateCommand, Candidate>
    {
        private readonly IRepository<Candidate> _candidateRepository;

        public CreateOrChangeCandidateHandler(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<Candidate> Handle(CreateOrChangeCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository
                .FindNoTrackingAsync(request.Candidate.Id, cancellationToken)
                ?? await _candidateRepository
                .GetAllAsNoTracking()
                .Where(e => e.Name == request.Candidate.Name
                && e.DateOfBirth == request.Candidate.DateOfBirth)
                .FirstOrDefaultAsync(cancellationToken);

            if (candidate == null)
            {
                await _candidateRepository.AddAsync(request.Candidate, cancellationToken);
            }
            else
            {
                request.Candidate.Id = candidate.Id;
                await _candidateRepository.Update(request.Candidate);
            }
            await _candidateRepository.SaveAsync(cancellationToken);
            return request.Candidate;
        }
    }
}

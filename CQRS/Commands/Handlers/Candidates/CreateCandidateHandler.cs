using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model;
using Microsoft.Extensions.Options;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;

        public CreateCandidateHandler(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            await _candidateRepository.AddAsync(request.Candidate);
            await _candidateRepository.SaveAsync();
            if (request.UploadedFile != null)
            {
                var candidate = await _candidateRepository
                    .GetAllAsNoTracking()
                    .FirstAsync(c => c == request.Candidate, cancellationToken);
            }
            await _candidateRepository.SaveAsync();
            return true;
        }
    }
}

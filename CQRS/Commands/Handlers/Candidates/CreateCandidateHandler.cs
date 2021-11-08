using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<CandidateVacancy> _candidateVacancyRepository;

        public CreateCandidateHandler(IRepository<Candidate> candidateRepository,
            IRepository<CandidateVacancy> candidateVacancyRepository)
        {
            _candidateRepository = candidateRepository;
            _candidateVacancyRepository = candidateVacancyRepository;
        }

        public async Task<bool> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            await _candidateRepository.AddAsync(request.Candidate);
            await _candidateRepository.SaveAsync();
            await _candidateVacancyRepository.AddAsync(
                new CandidateVacancy()
                {
                    CandidateId = request.Candidate.Id,
                    VacancyId = request.VacancyId });
            await _candidateVacancyRepository.SaveAsync();
            return true;
        }
    }
}

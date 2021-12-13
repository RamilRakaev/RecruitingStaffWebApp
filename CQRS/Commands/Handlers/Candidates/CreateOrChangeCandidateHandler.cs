using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
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
        private readonly IRepository<CandidateVacancy> _candidateVacancyRepository;
        private readonly IRepository<CandidateQuestionnaire> _candidateQuestionnaireRepository;

        public CreateOrChangeCandidateHandler(IRepository<Candidate> candidateRepository,
            IRepository<CandidateVacancy> candidateVacancyRepository,
            IRepository<CandidateQuestionnaire> candidateQuestionnaireRepository)
        {
            _candidateRepository = candidateRepository;
            _candidateVacancyRepository = candidateVacancyRepository;
            _candidateQuestionnaireRepository = candidateQuestionnaireRepository;
        }

        public async Task<Candidate> Handle(CreateOrChangeCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository
                .GetAllAsNoTracking()
                .Where(c => c.FullName == request.Candidate.FullName
                || c.Id == request.Candidate.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (candidate == null)
            {
                await _candidateRepository.AddAsync(request.Candidate, cancellationToken);
                await _candidateRepository.SaveAsync(cancellationToken);
                if (request.VacancyId != 0)
                {
                    await _candidateVacancyRepository.AddAsync(new()
                    {
                        CandidateId = request.Candidate.Id,
                        VacancyId = request.VacancyId,
                    },
                    cancellationToken);
                }
                if (request.QuestionnaireId != 0)
                {
                    await _candidateQuestionnaireRepository.AddAsync(new()
                    {
                        CandidateId = request.Candidate.Id,
                        QuestionnaireId = request.QuestionnaireId,
                    },
                    cancellationToken);
                }
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

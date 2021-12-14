using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class CreateCandidateByQuestionnaireHandler : IRequestHandler<CreateCandidateByQuestionnaireCommand, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;
        //private readonly IRepository<CandidateQuestionnaire> _candidateQuestionnaireRepository;
        private readonly IRepository<Questionnaire> _questionnaireRepository;
        //private readonly IRepository<CandidateVacancy> _vacancyRepository;

        public CreateCandidateByQuestionnaireHandler(IRepository<Candidate> candidateRepository,
            IRepository<Questionnaire> questionnaireRepository)
        {
            _candidateRepository = candidateRepository;
            //_candidateQuestionnaireRepository = candidateQuestionnaireRepository;
            _questionnaireRepository = questionnaireRepository;
            //_vacancyRepository = vacancyRepository;
        }

        public async Task<bool> Handle(CreateCandidateByQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            //await _candidateRepository.AddAsync(request.Candidate, cancellationToken);
            //await _candidateRepository.SaveAsync(cancellationToken);
            //await _candidateQuestionnaireRepository
            //    .AddAsync(new CandidateQuestionnaire()
            //    {
            //        CandidateId = request.Candidate.Id,
            //        QuestionnaireId = request.QuestionnaireId
            //    },
            //    cancellationToken);
            //var questionnaire = await _questionnaireRepository.FindAsync(request.QuestionnaireId, cancellationToken);
            //await _vacancyRepository
            //    .AddAsync(new CandidateVacancy()
            //{
            //    CandidateId = request.Candidate.Id,
            //    VacancyId = questionnaire.VacancyId
            //},
            //cancellationToken);
            //await _vacancyRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}

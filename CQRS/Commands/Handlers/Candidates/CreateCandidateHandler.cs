using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, Candidate>
    {
        private readonly IRepository<Candidate> _candidateRepository;
        //private readonly IRepository<CandidateVacancy> _candidateVacancyRepository;
        //private readonly IRepository<CandidateQuestionnaire> _candidateQuestionnaireRepository;

        public CreateCandidateHandler(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
            //_candidateVacancyRepository = candidateVacancyRepository;
            //_candidateQuestionnaireRepository = candidateQuestionnaireRepository;
        }

        public async Task<Candidate> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            //await _candidateRepository.AddAsync(request.Candidate, cancellationToken);
            //await _candidateRepository.SaveAsync(cancellationToken);
            //await _candidateVacancyRepository.AddAsync
            //(
            //    new CandidateVacancy()
            //    {
            //        CandidateId = request.Candidate.Id,
            //        VacancyId = request.VacancyId
            //    },
            //    cancellationToken
            //);
            //await _candidateVacancyRepository.SaveAsync(cancellationToken);
            //if(request.QuestionnaireId != 0)
            //{
            //    await _candidateQuestionnaireRepository.AddAsync(
            //        new CandidateQuestionnaire()
            //        {
            //            CandidateId = request.Candidate.Id,
            //            QuestionnaireId = request.QuestionnaireId
            //        }, cancellationToken
            //    );
            //    await _candidateQuestionnaireRepository.SaveAsync(cancellationToken);
            //}
            //return request.Candidate;
            return null;
        }
    }
}

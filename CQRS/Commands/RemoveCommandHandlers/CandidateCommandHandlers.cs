using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class CandidateCommandHandlers : AnswerCommandHandlers
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<CandidateVacancy> _candidateVacancyRepository;
        private readonly IRepository<CandidateQuestionnaire> _candidateQuestionnaireRepository;
        private readonly CandidateFileManagement rewriter;

        public CandidateCommandHandlers(IRepository<Answer> answerRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<CandidateVacancy> candidateVacancyRepository,
            IRepository<CandidateQuestionnaire> candidateQuestionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options) : base(answerRepository)
        {
            _candidateRepository = candidateRepository;
            _candidateVacancyRepository = candidateVacancyRepository;
            _candidateQuestionnaireRepository = candidateQuestionnaireRepository;
            rewriter = new CandidateFileManagement(fileRepository, options);
        }

        public async Task RemoveCandidate(int candidateId)
        {
            var candidate = await _candidateRepository.FindNoTrackingAsync(candidateId);
            foreach (var answerId in _answerRepository.GetAllAsNoTracking().Where(a => a.CandidateId == candidateId).Select(a => a.Id))
            {
                await RemoveAnswer(answerId);
            }
            if (candidate.PhotoId != null)
            {
                await rewriter.DeleteCandidateFile(candidate.PhotoId.Value);
            }
            await rewriter.DeleteCandidateFiles(candidateId);
            var candidateVacancies = _candidateVacancyRepository.GetAllAsNoTracking().Where(cv => cv.CandidateId == candidateId);
            if(candidateVacancies != null)
            {
                foreach (var candidateVacancy in candidateVacancies.ToArray())
                {
                    await _candidateVacancyRepository.RemoveAsync(candidateVacancy);
                }
            }
            var candidateQuestionnaires = _candidateQuestionnaireRepository.GetAllAsNoTracking().Where(cq => cq.CandidateId == candidateId);
            if (candidateQuestionnaires != null)
            {
                foreach (var candidateQuestionnaire in candidateQuestionnaires.ToArray())
                {
                    await _candidateQuestionnaireRepository.RemoveAsync(candidateQuestionnaire);
                }
            }
            await _candidateRepository.RemoveAsync(candidate);
            await _candidateRepository.SaveAsync();
        }
    }
}

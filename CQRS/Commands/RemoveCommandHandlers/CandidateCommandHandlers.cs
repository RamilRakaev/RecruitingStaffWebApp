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
        private readonly CandidateFilesRewriter rewriter;

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
            rewriter = new CandidateFilesRewriter(fileRepository, options);
        }

        public async Task RemoveCandidate(int candidateId)
        {
            var candidate = await _candidateRepository.FindNoTrackingAsync(candidateId);
            foreach(var answer in candidate.Answers)
            {
                await RemoveAnswer(answer.Id);
            }
            await rewriter.DeleteFile(candidate.Photo);
            foreach(var document in candidate.Documents)
            {
                await rewriter.DeleteFile(document);
            }
            foreach(var candidateVacancy in _candidateVacancyRepository.GetAllAsNoTracking().Where(cv => cv.CandidateId == candidateId))
            {
                await _candidateVacancyRepository.RemoveAsync(candidateVacancy);
            }
            foreach(var candidateQuestionnaire in _candidateQuestionnaireRepository.GetAllAsNoTracking().Where(cq => cq.CandidateId == candidateId))
            {
                await _candidateQuestionnaireRepository.RemoveAsync(candidateQuestionnaire);
            }
            await _candidateRepository.RemoveAsync(candidate);
            await _candidateRepository.SaveAsync();
        }
    }
}

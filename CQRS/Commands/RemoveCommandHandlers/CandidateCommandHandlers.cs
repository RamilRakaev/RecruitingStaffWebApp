using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class CandidateCommandHandlers : AnswerCommandHandlers
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<CandidateVacancy> _candidateVacancyRepository;
        private readonly IRepository<CandidateQuestionnaire> _candidateQuestionnaireRepository;
        private readonly IRepository<Option> _optionRepository;
        private readonly CandidateFileManagement rewriter;

        public CandidateCommandHandlers(IRepository<Answer> answerRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<CandidateVacancy> candidateVacancyRepository,
            IRepository<CandidateQuestionnaire> candidateQuestionnaireRepository,
            IRepository<Option> optionRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost) : base(answerRepository)
        {
            _candidateRepository = candidateRepository;
            _candidateVacancyRepository = candidateVacancyRepository;
            _candidateQuestionnaireRepository = candidateQuestionnaireRepository;
            _optionRepository = optionRepository;
            rewriter = new CandidateFileManagement(fileRepository, options, webHost);
        }

        public async Task RemoveCandidate(int candidateId, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.FindNoTrackingAsync(candidateId, cancellationToken);
            foreach (var answerId in _answerRepository.GetAllAsNoTracking().Where(a => a.CandidateId == candidateId).Select(a => a.Id).ToArray())
            {
                await RemoveAnswer(answerId, cancellationToken);
            }
            foreach (var option in _optionRepository.GetAllAsNoTracking().ToArray())
            {
                await _optionRepository.RemoveAsync(option);
            }
            await _optionRepository.SaveAsync(cancellationToken);

            await rewriter.DeleteCandidateFiles(candidateId, cancellationToken);

            var candidateVacancies = _candidateVacancyRepository.GetAllAsNoTracking().Where(cv => cv.CandidateId == candidateId).ToArray();
            if (candidateVacancies != null)
            {
                foreach (var candidateVacancy in candidateVacancies.ToArray())
                {
                    await _candidateVacancyRepository.RemoveAsync(candidateVacancy);
                }
            }
            var candidateQuestionnaires = _candidateQuestionnaireRepository.GetAllAsNoTracking().Where(cq => cq.CandidateId == candidateId).ToArray();
            if (candidateQuestionnaires != null)
            {
                foreach (var candidateQuestionnaire in candidateQuestionnaires.ToArray())
                {
                    await _candidateQuestionnaireRepository.RemoveAsync(candidateQuestionnaire);
                }
            }
            await _candidateRepository.RemoveAsync(candidate);
            await _candidateRepository.SaveAsync(cancellationToken);
        }
    }
}

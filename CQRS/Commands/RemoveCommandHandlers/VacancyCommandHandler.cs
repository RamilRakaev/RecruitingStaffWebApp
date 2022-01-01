using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class VacancyCommandHandler : QuestionnairesCommandHandlers
    {
        private readonly IRepository<Vacancy> _vacancyRepository;

        public VacancyCommandHandler(IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<QuestionCategory> questionCategoryRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IRepository<Vacancy> vacancyRepository,
            IWebHostEnvironment webHost) 
            : base(answerRepository,
                  questionRepository,
                  questionCategoryRepository,
                  questionnaireRepository,
                  fileRepository,
                  options,
                  webHost)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task RemoveVacancy(int vacancyId, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository.FindNoTrackingAsync(vacancyId, cancellationToken);
            foreach (var questionnare in _questionnaireRepository.GetAllAsNoTracking().Where(q => q.VacancyId == vacancyId).ToArray())
            {
                await RemoveQuestionnaire(questionnare.Id, cancellationToken);
            }
            await _vacancyRepository.RemoveAsync(vacancy);
            await _vacancyRepository.SaveAsync(cancellationToken);
        }
    }
}

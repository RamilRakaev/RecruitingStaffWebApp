using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class QuestionnairesCommandHandlers : QuestionCategoryRemoveHandler
    {
        protected readonly IRepository<Questionnaire> _questionnaireRepository;
        private readonly CandidateFileManagement rewriter;

        public QuestionnairesCommandHandlers(IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<QuestionCategory> questionCategoryRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost) 
            : base(answerRepository, questionRepository, questionCategoryRepository)
        {
            _questionnaireRepository = questionnaireRepository;
            rewriter = new CandidateFileManagement(fileRepository, options, webHost);
        }

        public async Task RemoveQuestionnaire(int questionnireId, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireRepository.FindNoTrackingAsync(questionnireId, cancellationToken);
            var questionCategoriesIds = 
                _questionCategoryRepository
                .GetAllAsNoTracking()
                .Where(qc => qc.QuestionnaireId == questionnireId)
                .Select(qc => qc.Id)
                .ToArray();
                foreach (var questionCategoryId in questionCategoriesIds)
                {
                    await RemoveQuestionCategory(questionCategoryId, cancellationToken);
                }
            
            await rewriter.DeleteQuestionnaireFiles(questionnireId, cancellationToken);
            await _questionnaireRepository.RemoveAsync(questionnaire);
            await _questionnaireRepository.SaveAsync(cancellationToken);
        }
    }
}

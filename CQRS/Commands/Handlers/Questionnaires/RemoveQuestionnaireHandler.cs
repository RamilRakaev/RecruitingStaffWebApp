using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class RemoveQuestionnaireHandler : QuestionnairesCommandHandlers, IRequestHandler<RemoveQuestionnaireCommand, bool>
    {
        public RemoveQuestionnaireHandler(
            IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<QuestionCategory> questionCategoryRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost)
            : base(answerRepository,
                  questionRepository,
                  questionCategoryRepository,
                  questionnaireRepository,
                  fileRepository,
                  options,
                  webHost)
        { }

        public async Task<bool> Handle(RemoveQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            await RemoveQuestionnaire(request.QuestionnaireId);
            return true;
        }
    }
}

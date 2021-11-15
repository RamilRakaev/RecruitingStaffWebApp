using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.QuestionCategories
{
    public class RemoveQuestionCategoryHandler : QuestionCategoryRemoveHandler, IRequestHandler<RemoveQuestionCategoryCommand, bool>
    {
        public RemoveQuestionCategoryHandler(
            IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<QuestionCategory> questionCategoryRepository)
            : base(answerRepository, questionRepository, questionCategoryRepository)
        { }

        public async Task<bool> Handle(RemoveQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            await RemoveQuestionCategory(request.QestionId, cancellationToken);
            return true;
        }
    }
}

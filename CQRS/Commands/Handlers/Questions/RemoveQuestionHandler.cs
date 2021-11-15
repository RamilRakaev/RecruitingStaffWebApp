using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questions
{
    public class RemoveQuestionHandler : QuestionCommandHandlers, IRequestHandler<RemoveQuestionCommand, bool>
    {
        public RemoveQuestionHandler(
            IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository) : base(answerRepository, questionRepository)
        { }

        public async Task<bool> Handle(RemoveQuestionCommand request, CancellationToken cancellationToken)
        {
            await RemoveQuestion(request.QestionId, cancellationToken);
            return true;
        }
    }
}

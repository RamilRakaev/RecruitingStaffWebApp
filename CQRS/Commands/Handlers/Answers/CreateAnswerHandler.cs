using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Answers
{
    public class CreateAnswerHandler : AnswerCommandHandlers, IRequestHandler<CreateAnswerCommand, bool>
    {
        public CreateAnswerHandler(IRepository<Answer> answerRepository) : base(answerRepository)
        { }

        public async Task<bool> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            await CreateAnswer(request.Answer, cancellationToken);
            return true;
        }
    }
}

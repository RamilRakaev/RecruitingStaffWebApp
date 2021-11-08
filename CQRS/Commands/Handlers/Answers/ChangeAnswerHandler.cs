using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Answers
{
    public class ChangeAnswerHandler : AnswerCommandHandlers, IRequestHandler<ChangeAnswerCommand, bool>
    {
        public ChangeAnswerHandler(IRepository<Answer> answerRepository) : base(answerRepository)
        { }

        public async Task<bool> Handle(ChangeAnswerCommand request, CancellationToken cancellationToken)
        {
            await ChangeAnswer(request.Answer);
            return true;
        }
    }
}

using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers
{
    public class RemoveAnswerCommand : IRequest<bool>
    {
        public RemoveAnswerCommand(int answerId)
        {
            AnswerId = answerId;
        }

        public int AnswerId { get; set; }
    }
}

using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers
{
    public class RemoveAnswerCommand : IRequest<bool>
    {
        public int AnswerId { get; set; }
    }
}

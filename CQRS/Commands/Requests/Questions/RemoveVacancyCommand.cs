using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions
{
    public class RemoveQuestionCommand : IRequest<bool>
    {
        public RemoveQuestionCommand(int qestionId)
        {
            QestionId = qestionId;
        }

        public int QestionId { get; set; }
    }
}

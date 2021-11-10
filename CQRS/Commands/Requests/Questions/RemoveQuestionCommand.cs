using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions
{
    public class RemoveQuestionCommand : IRequest<bool>
    {
        public RemoveQuestionCommand(int questionId)
        {
            QestionId = questionId;
        }

        public int QestionId { get; set; }
    }
}

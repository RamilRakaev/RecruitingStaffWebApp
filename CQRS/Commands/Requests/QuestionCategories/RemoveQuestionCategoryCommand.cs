using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories
{
    public class RemoveQuestionCategoryCommand : IRequest<bool>
    {
        public RemoveQuestionCategoryCommand(int qestionId)
        {
            QestionId = qestionId;
        }

        public int QestionId { get; set; }
    }
}

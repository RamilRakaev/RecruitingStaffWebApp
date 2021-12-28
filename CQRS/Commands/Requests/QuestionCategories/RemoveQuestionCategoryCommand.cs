using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories
{
    public class RemoveQuestionCategoryCommand : IRequest<bool>
    {
        public RemoveQuestionCategoryCommand(int qestionId)
        {
            QestionCategoryId = qestionId;
        }

        public int QestionCategoryId { get; set; }
    }
}

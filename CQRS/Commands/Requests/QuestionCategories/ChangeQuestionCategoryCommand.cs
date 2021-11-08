using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories
{
    public class ChangeQuestionCategoryCommand : IRequest<bool>
    {
        public ChangeQuestionCategoryCommand(QuestionCategory questionCategory)
        {
            QuestionCategory = questionCategory;
        }

        public QuestionCategory QuestionCategory { get; set; }
    }
}

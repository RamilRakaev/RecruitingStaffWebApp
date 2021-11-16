using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories
{
    public class CreateOrChangeQuestionCategoryCommand : IRequest<bool>
    {
        public CreateOrChangeQuestionCategoryCommand(QuestionCategory questionCategory)
        {
            QuestionCategory = questionCategory;
        }

        public QuestionCategory QuestionCategory { get; set; }
    }
}

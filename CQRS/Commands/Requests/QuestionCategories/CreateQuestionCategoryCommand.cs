using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories
{
    public class CreateQuestionCategoryCommand : IRequest<bool>
    {
        public CreateQuestionCategoryCommand(QuestionCategory questionCategory)
        {
            QuestionCategory = questionCategory;
        }

        public QuestionCategory QuestionCategory { get; set; }
    }
}

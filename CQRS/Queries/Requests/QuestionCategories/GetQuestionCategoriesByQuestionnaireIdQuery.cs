using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories
{
    public class GetQuestionCategoriesByQuestionnaireIdQuery : IRequest<QuestionCategory[]>
    {
        public GetQuestionCategoriesByQuestionnaireIdQuery(int questionCategoryId)
        {
            QuestionCategoryId = questionCategoryId;
        }

        public int QuestionCategoryId { get; set; }
    }
}

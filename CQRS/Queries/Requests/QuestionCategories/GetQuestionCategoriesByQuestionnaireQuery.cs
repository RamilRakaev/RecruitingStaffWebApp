using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories
{
    public class GetQuestionCategoriesByQuestionnaireQuery : IRequest<QuestionCategory[]>
    {
        public GetQuestionCategoriesByQuestionnaireQuery(int questionCategoryId)
        {
            QuestionCategoryId = questionCategoryId;
        }

        public int QuestionCategoryId { get; set; }
    }
}

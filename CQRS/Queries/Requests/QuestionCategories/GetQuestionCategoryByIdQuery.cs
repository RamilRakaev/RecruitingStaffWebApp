using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories
{
    public class GetQuestionCategoryByIdQuery : IRequest<QuestionCategory>
    {
        public GetQuestionCategoryByIdQuery(int questionCategoryId)
        {
            QuestionCategoryId = questionCategoryId;
        }

        public int QuestionCategoryId { get; set; }
    }
}

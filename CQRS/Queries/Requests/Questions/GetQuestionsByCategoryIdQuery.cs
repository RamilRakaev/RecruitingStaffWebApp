using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions
{
    public class GetQuestionsByCategoryIdQuery : IRequest<Question[]>
    {
        public GetQuestionsByCategoryIdQuery(int questionCategoryId)
        {
            QuestionCategoryId = questionCategoryId;
        }

        public int QuestionCategoryId { get; set; }
    }
}

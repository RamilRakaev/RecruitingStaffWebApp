using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers
{
    public class GetAnswersByQuestionCategoryQuery : IRequest<Answer[]>
    {
        public GetAnswersByQuestionCategoryQuery(int questionCategoryId)
        {
            QuestionCategoryId = questionCategoryId;
        }

        public int QuestionCategoryId { get; set; }
    }
}

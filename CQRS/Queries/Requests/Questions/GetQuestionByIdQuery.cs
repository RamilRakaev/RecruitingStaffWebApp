using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions
{
    public class GetQuestionByIdQuery : IRequest<Question>
    {
        public GetQuestionByIdQuery(int questionId)
        {
            QuestionId = questionId;
        }

        public int QuestionId { get; set; }
    }
}

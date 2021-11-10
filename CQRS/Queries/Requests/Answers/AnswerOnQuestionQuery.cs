using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers
{
    public class AnswerOnQuestionQuery : IRequest<Answer[]>
    {
        public AnswerOnQuestionQuery(int questionId)
        {
            QuestionId = questionId;
        }

        public int QuestionId { get; set; }
    }
}

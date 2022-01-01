using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers
{
    public class AnswersOnQuestionQuery : IRequest<Answer[]>
    {
        public AnswersOnQuestionQuery(int questionId)
        {
            QuestionId = questionId;
        }

        public int QuestionId { get; set; }
    }
}

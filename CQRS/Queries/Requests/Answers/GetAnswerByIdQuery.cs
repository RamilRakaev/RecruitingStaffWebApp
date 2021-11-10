using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers
{
    public class GetAnswerByIdQuery : IRequest<Answer>
    {
        public GetAnswerByIdQuery(int answerId)
        {
            AnswerId = answerId;
        }

        public int AnswerId { get; set; }
    }
}

using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Answers
{
    public class AnswerByCandidateIdQuery : IRequest<Answer>
    {
        public AnswerByCandidateIdQuery(int candidateId)
        {
            CandidateId = candidateId;
        }

        public int CandidateId { get; set; }
    }
}

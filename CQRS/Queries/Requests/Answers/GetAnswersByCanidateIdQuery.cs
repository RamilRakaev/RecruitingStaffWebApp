using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers
{
    public class GetAnswersByCanidateIdQuery : IRequest<Answer[]>
    {
        public GetAnswersByCanidateIdQuery(int candidateId)
        {
            CandidateId = candidateId;
        }

        public int CandidateId { get; set; }
    }
}

using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates
{
    public class ContainsCandidateQuery : IRequest<bool>
    {
        public ContainsCandidateQuery(string candidateName)
        {
            CandidateName = candidateName;
        }

        public ContainsCandidateQuery(int candidateId)
        {
            CandidateId = candidateId;
        }

        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
    }
}

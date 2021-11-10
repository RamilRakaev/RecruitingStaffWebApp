using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class RemoveCandidateCommand : IRequest<bool>
    {
        public RemoveCandidateCommand(int candidateId)
        {
            CandidateId = candidateId;
        }

        public int CandidateId { get; set; }
    }
}

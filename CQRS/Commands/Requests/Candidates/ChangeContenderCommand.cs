using Domain.Model.CandidateQuestionnaire;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class ChangeCandidateCommand : IRequest<bool>
    {
        public ChangeCandidateCommand(Candidate candidate)
        {
            Candidate = candidate;
        }

        public Candidate Candidate { get; set; }
    }
}

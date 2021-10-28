using Domain.Model;
using MediatR;

namespace CQRS.Commands.Requests.Candidates
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

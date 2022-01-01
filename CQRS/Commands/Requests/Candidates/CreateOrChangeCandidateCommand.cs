using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class CreateOrChangeCandidateCommand : IRequest<Candidate>
    {
        public CreateOrChangeCandidateCommand(Candidate candidate)
        {
            Candidate = candidate;
        }

        public Candidate Candidate { get; set; }
    }
}

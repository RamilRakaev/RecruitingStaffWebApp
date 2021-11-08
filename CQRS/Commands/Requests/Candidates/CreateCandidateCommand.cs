using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class CreateCandidateCommand : IRequest<bool>
    {
        public CreateCandidateCommand(Candidate candidate)
        {
            Candidate = candidate;
        }

        public Candidate Candidate { get; set; }
    }
}

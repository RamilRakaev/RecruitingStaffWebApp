using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class CreateCandidateCommand : IRequest<bool>
    {
        public CreateCandidateCommand(Candidate candidate, int vacancyId)
        {
            Candidate = candidate;
            VacancyId = vacancyId;
        }

        public Candidate Candidate { get; set; }
        public int VacancyId { get; set; }
    }
}

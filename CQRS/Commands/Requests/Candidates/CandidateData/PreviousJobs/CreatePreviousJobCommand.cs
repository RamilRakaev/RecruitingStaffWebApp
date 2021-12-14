using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.PreviousJobs
{
    public class CreatePreviousJobCommand : IRequest<PreviousJobPlacement>
    {
        public CreatePreviousJobCommand(PreviousJobPlacement previousJob)
        {
            PreviousJob = previousJob;
        }

        public PreviousJobPlacement PreviousJob { get; set; }
    }
}

using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.PreviousJobs
{
    public class CreatePreviousJobCommand : IRequest<PreviousJob>
    {
        public CreatePreviousJobCommand(PreviousJob previousJob)
        {
            PreviousJob = previousJob;
        }

        public PreviousJob PreviousJob { get; set; }
    }
}

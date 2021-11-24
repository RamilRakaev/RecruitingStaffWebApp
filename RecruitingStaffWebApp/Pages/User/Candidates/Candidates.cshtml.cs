using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class CandidatesModel : BasePageModel
    {
        public CandidatesModel(IMediator mediator, ILogger<CandidatesModel> logger) : base(mediator, logger)
        { }

        public Candidate[] Candidates { get; set; }
        public string MessageAboutDocumentsSource { get; set; }

        public async Task OnGet()
        {
            Candidates = await _mediator.Send(new GetCandidatesQuery());
            MessageAboutDocumentsSource = await _mediator.Send(new CheckDocumentsSourceCommand());
        }

        public async Task OnPost(int CandidateId)
        {
            await _mediator.Send(new RemoveCandidateCommand(CandidateId));
            Candidates = await _mediator.Send(new GetCandidatesQuery());
            MessageAboutDocumentsSource = await _mediator.Send(new CheckDocumentsSourceCommand());
        }
    }
}

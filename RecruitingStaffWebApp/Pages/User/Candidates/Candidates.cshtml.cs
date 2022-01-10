using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class CandidatesModel : BasePageModel
    {
        public CandidatesModel(IMediator mediator, ILogger<CandidatesModel> logger) : base(mediator, logger)
        { }

        public Candidate[] Candidates { get; set; }
        public string MessageAboutDocumentsSource
        {
            get; set;
        }

        public async Task OnGet(string messageAboutDocumentsSource)
        {
            Candidates = await _mediator.Send(new GetEntitiesQuery<Candidate>());
            MessageAboutDocumentsSource = messageAboutDocumentsSource;
            MessageAboutDocumentsSource ??= await _mediator.Send(new CheckDocumentsSourceQuery());
        }

        public async Task OnPost(int CandidateId)
        {
            await _mediator.Send(new RemoveCandidateCommand(CandidateId));
            Candidates = await _mediator.Send(new GetEntitiesQuery<Candidate>());
            MessageAboutDocumentsSource = await _mediator.Send(new CheckDocumentsSourceQuery());
        }
    }
}

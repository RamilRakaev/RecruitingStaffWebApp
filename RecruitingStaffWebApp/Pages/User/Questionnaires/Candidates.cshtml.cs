using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class CandidatesModel : BasePageModel
    {
        public CandidatesModel(IMediator mediator, ILogger<CandidatesModel> logger) : base(mediator, logger)
        {
        }

        public CandidateViewModel[] Candidates { get; set; }
        public int QuestionnaireId { get; set; }

        public async Task OnGet(int questionnaireId)
        {
            Candidates = GetViewModels<Candidate, CandidateViewModel>(
                await _mediator.Send(new GetCandidatesByQuestionnaireQuery(questionnaireId)));
            QuestionnaireId = questionnaireId;
        }

        public async Task OnPost(int questionnaireId, int candidateId)
        {
            await _mediator.Send(new RemoveCandidateCommand(candidateId));
            QuestionnaireId = questionnaireId;
            Candidates = GetViewModels<Candidate, CandidateViewModel>(
                 await _mediator.Send(new GetCandidatesByQuestionnaireQuery(questionnaireId)));
        }
    }
}

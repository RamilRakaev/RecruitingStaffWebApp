using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using RecruitingStaff.WebApp.ViewModels;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using RecruitingStaffWebApp.Pages.User.Candidates;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class AnswerAuthorModel : ConcreteCandidateModel
    {
        public AnswerAuthorModel(
            IMediator mediator,
            ILogger<BasePageModel> logger,
            IOptions<WebAppOptions> options)
            : base(mediator, logger, options)
        {
        }
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }

        public async Task OnGetAnswerAuthorStart(int candidateId, int questionnaireId, int questionId)
        {
            QuestionnaireId = questionnaireId;
            QuestionId = questionId;
            await base.OnGet(candidateId);
        }

        protected override async Task<CandidateOptionsViewModel> Initialization(int candidateId)
        {
            var candidateOptionsViewModel = await base.Initialization(candidateId);
            candidateOptionsViewModel.ControlPanelDisplaying = false;
            return candidateOptionsViewModel;
        }
    }
}

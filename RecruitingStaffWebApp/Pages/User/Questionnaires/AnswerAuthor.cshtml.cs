using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using RecruitingStaff.WebApp.ViewModels;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class AnswerAuthorModel : BasePageModel
    {
        public AnswerAuthorModel(IMediator mediator, ILogger<AnswerAuthorModel> logger) : base(mediator, logger)
        { }

        public CandidateViewModel Candidate { get; set; }
        public OptionViewModel[] Options { get; set; }
        public int QuestionId { get; set; }
        public string CandidatePhotoSource { get; set; }
        public int QuestionnaireId { get; set; }

        public async Task OnGet(int candidateId, int questionId, int questionnaireId)
        {
            QuestionId = questionId;
            Candidate = GetViewModel<Candidate, CandidateViewModel>(
                await _mediator.Send(new GetEntityByIdQuery<Candidate>(candidateId)));
            Options = GetViewModels<Option, OptionViewModel>(
                await _mediator.Send(new GetOptionsByCandidateIdQuery(candidateId)));
            CandidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
            QuestionnaireId = questionnaireId;
        }

        public async Task OnPost(int optionId, int candidateId, int questionId, int questionnaireId)
        {
            QuestionId = questionId;
            await _mediator.Send(new RemoveOptionCommand(optionId));
            Candidate = GetViewModel<Candidate, CandidateViewModel>(
                await _mediator.Send(new GetEntityByIdQuery<Candidate>(candidateId)));
            Options = GetViewModels<Option, OptionViewModel>(
                await _mediator.Send(new GetOptionsByCandidateIdQuery(candidateId)));
            CandidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
            QuestionnaireId = questionnaireId;
        }
    }
}

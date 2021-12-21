using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.WebApp.ViewModels;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Questionnaires
{
    public class OptionFormModel : BasePageModel
    {
        public OptionFormModel(IMediator mediator, ILogger<OptionFormModel> logger) : base(mediator, logger)
        {
            Option = new OptionViewModel();
        }

        public OptionViewModel Option { get; set; }
        public int QuestionId { get; set; }

        public void OnGet(int candidateId, int questionId, string propertyName = "", string value = "")
        {
            Option.CandidateId = candidateId;
            QuestionId = questionId;
            Option.Name = propertyName;
            Option.Value = value;
        }

        public async Task<IActionResult> OnPost(OptionViewModel option, int questionId)
        {
            var optionEntity = GetEntity<Option, OptionViewModel>(option);
            await _mediator.Send(new CreateOrChangeEntityByKeysCommand<Option>(optionEntity));
            return RedirectToPage("ConcreteCandidate", new
            {
                candidateId = option.CandidateId.Value,
                questionId
            });
        }
    }
}

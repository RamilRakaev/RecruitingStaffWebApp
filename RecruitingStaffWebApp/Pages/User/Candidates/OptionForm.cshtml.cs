using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.WebApp.ViewModels;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class OptionFormModel : BasePageModel
    {
        public OptionFormModel(IMediator mediator, ILogger<OptionFormModel> logger) : base(mediator, logger)
        {
            Option = new OptionViewModel();
        }

        public OptionViewModel Option { get; set; }

        public void OnGet(int? CandidateId, string propertyName = "", string value = "")
        {
            Option.Name = propertyName;
            Option.CandidateId = CandidateId;
            Option.Value = value;
        }

        public async Task<IActionResult> OnPost(OptionViewModel option)
        {
            var optionEntity = GetEntity<Option, OptionViewModel>(option);
            await _mediator.Send(new CreateOrEditOptionCommand(optionEntity));
            if(option.CandidateId != null)
            {
                return RedirectToPage("ConcreteCandidate", new { CandidateId = option.CandidateId.Value });
            }
            return RedirectToPage("Candidates");
        }
    }
}

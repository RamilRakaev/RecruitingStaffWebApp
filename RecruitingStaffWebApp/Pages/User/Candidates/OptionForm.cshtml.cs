using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.WebApp.ViewModels;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class OptionFormModel : BasePageModel
    {
        public OptionFormModel(IMediator mediator, ILogger<OptionFormModel> logger) : base(mediator, logger)
        {
            OptionViewModel = new OptionViewModel();
        }

        public OptionViewModel OptionViewModel { get; set; }

        public void OnGet(int? CandidateId, string propertyName = "", string value = "")
        {
            OptionViewModel.Name = propertyName;
            OptionViewModel.CandidateId = CandidateId;
            OptionViewModel.Value = value;
        }

        public async Task<IActionResult> OnPost(OptionViewModel optionViewModel)
        {
            var optionEntity = GetEntity<Option, OptionViewModel>(optionViewModel);
            await _mediator.Send(new CreateOrChangeEntityCommand<Option>(optionEntity));
            if(optionViewModel.CandidateId != null)
            {
                return RedirectToPage("ConcreteCandidate", new { CandidateId = optionViewModel.CandidateId.Value });
            }
            return RedirectToPage("Candidates");
        }
    }
}

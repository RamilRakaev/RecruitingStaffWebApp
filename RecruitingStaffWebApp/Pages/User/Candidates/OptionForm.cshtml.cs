using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class OptionFormModel : BasePageModel
    {
        public OptionFormModel(IMediator mediator, ILogger<OptionFormModel> logger) : base(mediator, logger)
        {
            Option = new Option();
        }

        public Option Option { get; set; }

        public void OnGet(int? CandidateId, string propertyName = "", string value = "")
        {
            Option.PropertyName = propertyName;
            Option.CandidateId = CandidateId;
            Option.Value = value;
        }

        public async Task<IActionResult> OnPost(Option option)
        {
            await _mediator.Send(new CreateOrEditOptionCommand(option));
            if(option.CandidateId != null)
            {
                return RedirectToPage("ConcreteCandidate", new { CandidateId = option.CandidateId.Value });
            }
            return RedirectToPage("Candidates");
        }
    }
}

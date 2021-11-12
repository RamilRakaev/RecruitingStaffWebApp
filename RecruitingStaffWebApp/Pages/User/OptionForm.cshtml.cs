using System.Threading.Tasks;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RecruitingStaffWebApp.Pages.User
{
    public class OptionFormModel : BasePageModel
    {
        public OptionFormModel(IMediator mediator, ILogger<OptionFormModel> logger) : base(mediator, logger)
        {
            Option = new Option();
        }

        public Option Option { get; set; }

        public async Task<IActionResult> OnGet(int? CandidateId, string propertyName = "", string value = "")
        {
            Option.PropertyName = propertyName;
            Option.CandidateId = CandidateId;
            Option.Value = value;
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(Option option)
        {
            await _mediator.Send(new CreateOrEditOptionCommand(option));
            if(option.CandidateId != null)
            {
                return RedirectToPage("/User/ConcreteCandidate", new { CandidateId = option.CandidateId.Value });
            }
            return RedirectToPage("/User/Candidates");
        }
    }
}

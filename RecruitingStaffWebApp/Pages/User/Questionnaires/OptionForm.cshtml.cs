using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Questionnaires
{
    public class OptionFormModel : BasePageModel
    {
        public OptionFormModel(IMediator mediator, ILogger<OptionFormModel> logger) : base(mediator, logger)
        {
            Option = new Option();
        }

        public Option Option { get; set; }
        public int QuestionId { get; set; }

        public void OnGet(int candidateId, int questionId, string propertyName = "", string value = "")
        {
            Option.PropertyName = propertyName;
            Option.CandidateId = candidateId;
            Option.Value = value;
            QuestionId = questionId;
        }

        public async Task<IActionResult> OnPost(Option option, int questionId)
        {
            await _mediator.Send(new CreateOrEditOptionCommand(option));
            return RedirectToPage("ConcreteCandidate", new
            {
                candidateId = option.CandidateId.Value,
                questionId
            });
        }
    }
}

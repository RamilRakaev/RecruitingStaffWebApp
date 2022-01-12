using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class CompletedQuestionnaireParseModel : BasePageModel
    {
        public CompletedQuestionnaireParseModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public SelectList QuestionnaireTypes { get; set; }
        public int CandidateId { get; set; }

        public async Task OnGet(int candidateId)
        {
            CandidateId = candidateId;
            QuestionnaireTypes = new(
                await _mediator.Send(
                new GetJobQuestionnairesDictionaryQuery()),
                "Key",
                "Value");
        }

        public async Task<IActionResult> OnPost(IFormFile formFile, int jobQuestionnaire, int candidateId)
        {
            if (await _mediator.Send(new DocumentParseCommand(formFile, jobQuestionnaire, false, candidateId)))
            {
                return RedirectToPage("/User/Candidates/Candidates");
            }
            ModelState.AddModelError("", "�� ������� ���������������� ��������");
            QuestionnaireTypes = new(
               await _mediator.Send(
               new GetJobQuestionnairesDictionaryQuery()),
               "Key",
               "Value");
            return Page();
        }
    }
}

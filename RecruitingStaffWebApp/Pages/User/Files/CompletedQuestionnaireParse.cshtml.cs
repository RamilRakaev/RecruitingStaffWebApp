using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaff.WebApp.ViewModels.Parse;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class CompletedQuestionnaireParseModel : BasePageModel
    {
        public int _questionnaireId;
        public string _redirect = "/User/Candidates/Candidates";
        public CompletedQuestionnaireParseModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        //public SelectList QuestionnaireTypes { get; set; }
        public int CandidateId { get; set; }
        public DocumentParseViewModel DocumentParseViewModel { get; set; }

        public void OnGetParseQuestionnaireExample()
        {
            DocumentParseViewModel = new()
            {
                IsCompletedQuestionnaire = false,
            };
            //QuestionnaireTypes = new(
            //    await _mediator.Send(
            //    new GetJobQuestionnairesDictionaryQuery()),
            //    "Key",
            //    "Value");
        }

        public void OnGetParseQuestionnaireCompletedByCandidate(int candidateId, int questionnaireId)
        {
            DocumentParseViewModel = new()
            {
                CandidateId = candidateId,
                QuestionnaireId = questionnaireId,
                IsCompletedQuestionnaire = true,
            };
        }

        public void OnGetParseCompletedQuestionnaire()
        {
            DocumentParseViewModel = new()
            {
                IsCompletedQuestionnaire = true,
            };
        }

        public async Task<IActionResult> OnPost(DocumentParseViewModel documentParseViewModel)
        {
            if (await _mediator.Send(
                new DocumentParseCommand(
                documentParseViewModel.FormFile,
                documentParseViewModel.IsCompletedQuestionnaire,
                documentParseViewModel.CandidateId,
                documentParseViewModel.QuestionnaireId)))
            {
                return RedirectToPage(_redirect);
            }
            ModelState.AddModelError("", "Не удалось проанализировать документ");
            DocumentParseViewModel = documentParseViewModel;
            //QuestionnaireTypes = new(
            //   await _mediator.Send(
            //   new GetJobQuestionnairesDictionaryQuery()),
            //   "Key",
            //   "Value");
            return Page();
        }
    }
}

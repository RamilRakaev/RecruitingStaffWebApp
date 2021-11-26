using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaffWebApp.Pages.User;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class DocumentFormModel : BasePageModel
    {
        public DocumentFormModel(IMediator mediator, ILogger<DocumentFormModel> logger) : base(mediator, logger)
        { }

        public SelectList QuestionnaireTypes { get; set; }

        public void OnGet()
        {
            var questionnaires = Enum.GetValues(typeof(JobQuestionnaire));
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for(int i = 0; i < questionnaires.Length; i++)
            {
                dictionary.Add(i, questionnaires.GetValue(i).ToString());
            }
            QuestionnaireTypes = new SelectList(dictionary, "Key", "Value");
        }

        public async Task<IActionResult> OnPost(IFormFile formFile, int jobQuestionnaire)
        {
            await _mediator.Send(new DocumentParseCommand(formFile, jobQuestionnaire));
            return RedirectToPage("/User/Candidates/Candidates");
        }
    }
}

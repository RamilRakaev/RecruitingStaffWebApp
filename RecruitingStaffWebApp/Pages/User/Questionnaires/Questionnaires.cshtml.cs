using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaffWebApp.Pages.User;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionnairesModel : BasePageModel
    {
        public QuestionnairesModel(IMediator mediator, ILogger<QuestionnairesModel> logger) : base(mediator, logger)
        {
        }

        public Questionnaire[] Questionnaires { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Questionnaires = await _mediator.Send(new GetQuestionnairesQuery());
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(int questionnaireId)
        {
            await _mediator.Send(new RemoveQuestionnaireCommand(questionnaireId));
            Questionnaires = await _mediator.Send(new GetQuestionnairesQuery());
            return await RightVerification();
        }
    }
}

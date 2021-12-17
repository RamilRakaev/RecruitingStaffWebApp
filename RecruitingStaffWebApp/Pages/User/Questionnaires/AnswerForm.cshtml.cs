using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class AnswerFormModel : BasePageModel
    {
        public AnswerFormModel(IMediator mediator, ILogger<AnswerFormModel> logger) : base(mediator, logger)
        {
        }

        public int QuestionnaireId { get; set; }
        public AnswerViewModel Answer { get; set; }
        public Candidate[] Candidates { get; set; }
        public string Message { get; set; } = "�������� ���������";

        public async Task OnGet(int? answerId, int questionId, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            Candidates = Array.Empty<Candidate>();
            if (answerId == null)
            {
                Answer = new AnswerViewModel() { QuestionId = questionId };
            }
            else
            {
                Answer = new AnswerViewModel(await _mediator.Send(new GetAnswerByIdQuery(answerId.Value)));
            }
        }

        public async Task OnPostSearchCandidates(string nameFragment, AnswerViewModel answer, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            Candidates = await _mediator.Send(new GetCandidatesByNameFragmentQuery(nameFragment));
            Message = "���������� � ����� ������ �� ����������";
            Answer = answer;
        }

        public async Task<IActionResult> OnPostCreateAnswer(AnswerViewModel answer, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            await _mediator.Send(new CreateOrChangeByViewModelCommand(answer));
            return RedirectToPage("AnswersOnQuestion", new { questionId = answer.QuestionId, questionnaireId });
        }
    }
}

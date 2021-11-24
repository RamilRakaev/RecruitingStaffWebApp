using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
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

        public Answer Answer { get; set; }
        public Candidate[] Candidates { get; set; }
        public string Message { get; set; } = "Выберите кандидата";

        public async Task OnGet(int? answerId, int questionId)
        {
            Candidates = Array.Empty<Candidate>();
            if (answerId == null)
            {
                Answer = new Answer() { QuestionId = questionId };
            }
            else
            {
                Answer = await _mediator.Send(new GetAnswerByIdQuery(answerId.Value));
            }
        }

        public async Task OnPostSearchCandidates(string nameFragment, Answer answer)
        {
            Candidates = await _mediator.Send(new GetCandidatesByNameFragmentQuery(nameFragment));
            Message = "Кандидатов с таким именем не существует";
            Answer = answer;
        }

        public async Task<IActionResult> OnPostCreateAnswer(Answer answer)
        {
            await _mediator.Send(new CreateOrChangeAnswerCommand(answer));
            return RedirectToPage("QuestionsByQuestionCategory", new { questionId = answer.QuestionId });
        }
    }
}

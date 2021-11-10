using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
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
        public AnswerFormModel(IMediator mediator) : base(mediator)
        {
        }

        public Answer Answer { get; set; }
        public Candidate[] Candidates { get; set; }

        public async Task<IActionResult> OnGet(int? answerId, int questionId)
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
            return await RightVerification();
        }

        public async Task OnPostSearchCandidates(string candidateName, int questionId)
        {
            Candidates = await _mediator.Send(new GetCandidatesByNameFragmentQuery(candidateName));
            Answer = new Answer() { QuestionId = questionId };
        }

        public async Task<IActionResult> OnPostCreateAnswer(Answer answer)
        {
            await _mediator.Send(new CreateOrChangeAnswerCommand(answer));
            return RedirectToPage("AnswersOnQuestion", new { questionId = answer.QuestionId });
        }
    }
}

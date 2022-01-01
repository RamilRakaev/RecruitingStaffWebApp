using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
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
        public AnswerViewModel AnswerViewModel { get; set; }
        public Candidate[] Candidates { get; set; }
        public string Message { get; set; } = "Выберите кандидата";

        public async Task OnGet(int? answerId, int questionId, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            Candidates = Array.Empty<Candidate>();
            if (answerId == null)
            {
                AnswerViewModel = new AnswerViewModel() { QuestionId = questionId };
            }
            else
            {
                AnswerViewModel = GetViewModel<Answer, AnswerViewModel>(
                    await _mediator.Send(new GetAnswerByIdQuery(answerId.Value)));
            }
        }

        public async Task OnPostSearchCandidates(string nameFragment, AnswerViewModel answerViewModel, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            Candidates = await _mediator.Send(new GetCandidatesByNameFragmentQuery(nameFragment));
            Message = "Кандидатов с таким именем не существует";
            AnswerViewModel = answerViewModel;
        }

        public async Task<IActionResult> OnPostCreateAnswer(AnswerViewModel answerViewModel, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            var answerEntity = GetEntity<Answer, AnswerViewModel>(answerViewModel);
            if(answerViewModel.Id == 0)
            {
                await _mediator.Send(new CreateEntityCommand<Answer>(answerEntity));
            }
            else
            {
                await _mediator.Send(new ChangeEntityCommand<Answer>(answerEntity));
            }
            return RedirectToPage("AnswersOnQuestion", new { questionId = answerViewModel.QuestionId, questionnaireId });
        }
    }
}

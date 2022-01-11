using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
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
        public string Message { get; set; } = "Выберите кандидата";

        public async Task OnGet(int? answerId, int questionId, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            if (answerId == null)
            {
                AnswerViewModel = new AnswerViewModel() { QuestionId = questionId };
            }
            else
            {
                var answerEntity = await _mediator.Send(new GetAnswerByIdQuery(answerId.Value));
                var config = new MapperConfiguration(c => c.CreateMap<Answer, AnswerViewModel>());
                var mapper = new Mapper(config);
                AnswerViewModel = mapper.Map<AnswerViewModel>(answerEntity);
                    
            }
            AnswerViewModel.CandidateViewModels = Array.Empty<CandidateViewModel>();
        }

        public async Task OnPostSearchCandidates(AnswerViewModel answerViewModel, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            Message = "Кандидатов с таким именем не существует";
            AnswerViewModel = await SearchCandidates(answerViewModel);
        }

        public async Task<IActionResult> OnPostCreateAnswer(AnswerViewModel answerViewModel, int questionnaireId)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireId = questionnaireId;
                var config = new MapperConfiguration(c => c.CreateMap<AnswerViewModel, Answer>());
                var mapper = new Mapper(config);
                var answerEntity = mapper.Map<Answer>(answerViewModel);
                if (answerViewModel.Id == 0)
                {
                    await _mediator.Send(new CreateEntityCommand<Answer>(answerEntity));
                }
                else
                {
                    await _mediator.Send(new ChangeEntityCommand<Answer>(answerEntity));
                }
                return RedirectToPage("AnswersOnQuestion", new { questionId = answerViewModel.QuestionId, questionnaireId });
            }
            QuestionnaireId = questionnaireId;
            AnswerViewModel = await SearchCandidates(answerViewModel);
            ModelState.AddModelError("", "Неправильно введены данные");
            return Page();
        }

        private async Task<AnswerViewModel> SearchCandidates(AnswerViewModel answerViewModel)
        {
            var candidateEntities = await _mediator.Send(new GetCandidatesByNameFragmentQuery(answerViewModel.NameFragmentOfCandidate));
            var config = new MapperConfiguration(c => c.CreateMap<Candidate, CandidateViewModel>());
            var mapper = new Mapper(config);
            var candidateViewModels = mapper.Map<CandidateViewModel[]>(candidateEntities);
            answerViewModel.CandidateViewModels = candidateViewModels;
            return answerViewModel;
        }
    }
}

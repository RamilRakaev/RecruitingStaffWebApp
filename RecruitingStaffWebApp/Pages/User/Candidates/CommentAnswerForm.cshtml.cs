using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates
{
    public class AnswerEvaluationFormModel : BasePageModel
    {
        public AnswerEvaluationFormModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public AnswerViewModel AnswerViewModel { get; set; }

        public async Task OnGet(int answerId)
        {
            var answerEntity = await _mediator.Send(new GetEntityByIdQuery<Answer>(answerId));
            var config = new MapperConfiguration(c => c.CreateMap<Answer, AnswerViewModel>());
            var mapper = new Mapper(config);
            AnswerViewModel = mapper.Map<AnswerViewModel>(answerEntity);
        }

        public async Task<IActionResult> OnPost(AnswerViewModel answerViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<AnswerViewModel, Answer>());
                var mapper = new Mapper(config);
                var answerEntity = mapper.Map<Answer>(answerViewModel);

                await _mediator.Send(new ChangeEntityCommand<Answer>(answerEntity));
                return RedirectToPage("CandidateAnswers", new { candidateId = answerEntity.CandidateId });
            }
            ModelState.AddModelError("", "Неправильно введены данные");
            AnswerViewModel = answerViewModel;
            return Page();
        }
    }
}

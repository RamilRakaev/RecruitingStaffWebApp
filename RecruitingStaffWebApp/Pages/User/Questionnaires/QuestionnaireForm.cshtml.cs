using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionnaireFormModel : BasePageModel
    {
        public QuestionnaireFormModel(IMediator mediator, ILogger<QuestionnaireFormModel> logger) : base(mediator, logger)
        {
        }

        public QuestionnaireViewModel QuestionnaireViewModel { get; set; }

        public async Task<IActionResult> OnGet(int? questionnaireId)
        {
            if (questionnaireId == null)
            {
                QuestionnaireViewModel = new QuestionnaireViewModel();
            }
            else
            {
                var questionnaireEntity = await _mediator.Send(new GetEntityByIdQuery<Questionnaire>(questionnaireId.Value));
                var config = new MapperConfiguration(c => c.CreateMap<Questionnaire, QuestionnaireViewModel>());
                var mapper = new Mapper(config);
                QuestionnaireViewModel = mapper.Map<QuestionnaireViewModel>(questionnaireEntity);
            }
            QuestionnaireViewModel.VacanciesSelectList =
                new SelectList(await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
            if (QuestionnaireViewModel.VacanciesSelectList.Any() == false)
            {
                return RedirectToPage("Questionnaires", new { messageAboutDocumentsSource = "Не введены вакансии" });
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(QuestionnaireViewModel questionnaireViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<QuestionnaireViewModel, Questionnaire>());
                var mapper = new Mapper(config);
                var questionnaireEntity = mapper.Map<Questionnaire>(questionnaireViewModel);
                if (questionnaireViewModel.Id == 0)
                {
                    await _mediator.Send(new CreateEntityCommand<Questionnaire>(questionnaireEntity));
                    await _mediator.Send(new TryCreateMapCommand<VacancyQuestionnaire>(questionnaireViewModel.VacancyId, questionnaireEntity.Id));
                }
                else
                {
                    await _mediator.Send(new ChangeEntityCommand<Questionnaire>(questionnaireEntity));
                }
                return RedirectToPage("Questionnaires");
            }
            ModelState.AddModelError("", "Неправильно введены данные");
            QuestionnaireViewModel = questionnaireViewModel;
            QuestionnaireViewModel.VacanciesSelectList =
                new SelectList(await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
            return Page();
        }
    }
}

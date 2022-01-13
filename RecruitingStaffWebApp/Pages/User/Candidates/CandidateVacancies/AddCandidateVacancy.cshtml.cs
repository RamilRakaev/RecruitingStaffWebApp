using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.Statuses
{
    public class AddCandidateVacancyModel : BasePageModel
    {
        public AddCandidateVacancyModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public CandidateVacancyViewModel CandidateVacancyViewModel { get; set; }

        public async Task OnGet(int candidateId)
        {
            CandidateVacancyViewModel = new();
            CandidateVacancyViewModel.FirstEntityId = candidateId;
            CandidateVacancyViewModel.VacanciesSelectList =
                new (await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
            var statuses = await _mediator.Send(new GetValuesQuery(typeof(CandidateStatus)));
            CandidateVacancyViewModel.CandidateStatusSelectList = new(statuses, "Key", "Value");
        }

        public async Task<IActionResult> OnPost(CandidateVacancyViewModel candidateVacancyViewModel)
        {
            if (ModelState.IsValid)
            {
                var candidateVacancyEntity =
                    await _mediator.Send(new CreateMapCommand<CandidateVacancy>(
                    candidateVacancyViewModel.FirstEntityId,
                    candidateVacancyViewModel.SecondEntityId));
                if(candidateVacancyEntity == null)
                {
                    return await Failure(candidateVacancyViewModel, "Привязка на эту вакансию уже существует");
                }
                candidateVacancyEntity.CandidateStatus = (CandidateStatus)candidateVacancyViewModel.CandidateStatusIndex;
                await _mediator.Send(new ChangeEntityCommand<CandidateVacancy>(candidateVacancyEntity));
                return RedirectToPage("CandidateVacancyStatuses", new { candidateId = candidateVacancyEntity.FirstEntityId });
            }
            return await Failure(candidateVacancyViewModel, "Неверно введены данные");
        }

        private async Task<IActionResult> Failure(CandidateVacancyViewModel candidateVacancyViewModel, string message)
        {
            ModelState.AddModelError("", message);
            CandidateVacancyViewModel = candidateVacancyViewModel;
            CandidateVacancyViewModel.VacanciesSelectList =
                new(await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
            var statuses = await _mediator.Send(new GetValuesQuery(typeof(CandidateStatus)));
            CandidateVacancyViewModel.CandidateStatusSelectList = new(statuses, "Key", "Value");
            return Page();
        }
    }
}

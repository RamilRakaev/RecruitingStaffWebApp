using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using RecruitingStaff.WebApp.ViewModels;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Vacancies
{
    public class ConcreteVacancyModel : BasePageModel
    {
        public ConcreteVacancyModel(IMediator mediator, ILogger<ConcreteVacancyModel> logger) : base(mediator, logger)
        {
        }

        public VacancyViewModel Vacancy { get; set; }

        public async Task OnGet(int vacancyId)
        {
            Vacancy = GetViewModel<Vacancy, VacancyViewModel>(
                await _mediator.Send(new GetVacancyQuery(vacancyId)));
        }

        public async Task<IActionResult> OnPost(int vacancyId)
        {
            await _mediator.Send(new RemoveVacancyCommand(vacancyId));
            return RedirectToPage("Vacancies");
        }
    }
}

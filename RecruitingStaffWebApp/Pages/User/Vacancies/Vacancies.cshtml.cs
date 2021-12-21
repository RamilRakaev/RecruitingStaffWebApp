using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using RecruitingStaff.WebApp.ViewModels;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Vacancies
{
    public class VacanciesModel : BasePageModel
    {
        public VacanciesModel(IMediator mediator, ILogger<VacanciesModel> logger) : base(mediator, logger)
        {
        }

        public VacancyViewModel[] Vacancies { get; set; }

        public async Task OnGet()
        {
            Vacancies = GetViewModels<Vacancy, VacancyViewModel>(
                await _mediator.Send(new GetVacanciesQuery()));
        }

        public async Task<IActionResult> OnPost(int vacancyId)
        {
            await _mediator.Send(new RemoveVacancyCommand(vacancyId));
            return RedirectToPage("Vacancies");
        }
    }
}

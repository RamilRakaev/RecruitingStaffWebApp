using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;

namespace RecruitingStaffWebApp.Pages.User.Vacancies
{
    public class VacanciesModel : BasePageModel
    {
        public VacanciesModel(IMediator mediator, ILogger<VacanciesModel> logger) : base(mediator, logger)
        {
        }

        public Vacancy[] Vacancies { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Vacancies = await _mediator.Send(new GetVacanciesQuery());
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(int vacancyId)
        {
            await _mediator.Send(new RemoveVacancyCommand(vacancyId));
            return RedirectToPage("Vacancies");
        }
    }
}

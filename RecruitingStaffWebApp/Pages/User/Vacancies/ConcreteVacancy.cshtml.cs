using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Vacancies
{
    public class ConcreteVacancyModel : BasePageModel
    {
        public ConcreteVacancyModel(IMediator mediator) : base(mediator)
        {
        }

        public Vacancy Vacancy { get; set; }

        public async Task<IActionResult> OnGet(int vacancyId)
        {
            Vacancy = await _mediator.Send(new GetVacancyQuery(vacancyId));
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(int vacancyId)
        {
            await _mediator.Send(new RemoveVacancyCommand(vacancyId));
            return RedirectToPage("Vacancies");
        }
    }
}

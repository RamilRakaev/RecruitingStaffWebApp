using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Vacancies
{
    public class VacancyFormModel : BasePageModel
    {
        public VacancyFormModel(IMediator mediator) : base(mediator)
        { }

        public Vacancy Vacancy { get; set; }

        public async Task<IActionResult> OnGet(int? vacancyId)
        {
            if (vacancyId == null)
            {
                Vacancy = new Vacancy();
            }
            else
            {
                Vacancy = await _mediator.Send(new GetVacancyQuery(vacancyId.Value));
            }
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(Vacancy vacancy)
        {
            await _mediator.Send(new CreateOrChangeVacancyCommand(vacancy));
            return RedirectToPage("/User/Vacancies/Vacancies");
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Vacancies
{
    public class VacancyFormModel : BasePageModel
    {
        public VacancyFormModel(IMediator mediator, ILogger<VacancyFormModel> logger) : base(mediator, logger)
        { }

        public Vacancy Vacancy { get; set; }

        public async Task OnGet(int? vacancyId)
        {
            if (vacancyId == null)
            {
                Vacancy = new Vacancy();
            }
            else
            {
                Vacancy = await _mediator.Send(new GetVacancyQuery(vacancyId.Value));
            }
        }

        public async Task<IActionResult> OnPost(Vacancy vacancy)
        {
            await _mediator.Send(new CreateOrChangeVacancyCommand(vacancy));
            return RedirectToPage("/User/Vacancies/Vacancies");
        }
    }
}

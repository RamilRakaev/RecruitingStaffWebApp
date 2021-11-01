using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;

namespace RecruitingStaffWebApp.Pages.User.Vacancies
{
    public class VacanciesModel : BasePageModel
    {
        public VacanciesModel(IMediator mediator) : base(mediator)
        {
        }

        public Vacancy[] Vacancies { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Vacancies = await _mediator.Send(new GetVacanciesQuery());
            return await RightVerification();
        }
    }
}

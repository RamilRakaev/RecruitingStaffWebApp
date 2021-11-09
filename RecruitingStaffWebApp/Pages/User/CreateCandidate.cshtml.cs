using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User
{
    public class CreateCandidateModel : BasePageModel
    {
        public CreateCandidateModel(IMediator mediator) : base(mediator)
        {
        }

        public SelectList Vacancies { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Vacancies = new SelectList(await _mediator.Send(new GetVacanciesQuery()), "Id", "Name");
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(Candidate candidate, int vacancyId)
        {
            await _mediator.Send(new CreateCandidateCommand(candidate, vacancyId));
            return RedirectToPage("/User/Candidates");
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using RecruitingStaff.WebApp.ViewModels;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class CreateCandidateModel : BasePageModel
    {
        public CreateCandidateModel(IMediator mediator, ILogger<CreateCandidateModel> logger) : base(mediator, logger)
        {
        }

        public SelectList Vacancies { get; set; }

        public async Task OnGet()
        {
            Vacancies = new SelectList(await _mediator.Send(new GetVacanciesQuery()), "Id", "Name");
        }

        public async Task<IActionResult> OnPost(CandidateViewModel candidate, int vacancyId)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateOrChangeByViewModelCommand(candidate));
            }
            return RedirectToPage("Candidates");
        }
    }
}

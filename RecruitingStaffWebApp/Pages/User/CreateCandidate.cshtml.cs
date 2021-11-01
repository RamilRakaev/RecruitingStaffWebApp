using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaffWebApp.Pages.User;

namespace RecruitingStaff.WebApp.Pages.User
{
    public class CreateCandidateModel : BasePageModel
    {
        public CreateCandidateModel(IMediator mediator) : base(mediator)
        {
        }

        public Candidate Candidate { get; set; }
        public SelectList Questionnaires { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Candidate = new Candidate();
            Questionnaires = new SelectList(await _mediator.Send(new GetQuestionnairesCommand()), "Id", "Vacancy.Name");
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(Candidate candidate, IFormFile file)
        {
            await _mediator.Send(new CreateCandidateCommand(candidate, file));
            return RedirectToPage("Candidates");
        }
    }
}

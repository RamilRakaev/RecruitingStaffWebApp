using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class CandidatePhotoFormModel : BasePageModel
    {
        public CandidatePhotoFormModel(IMediator mediator, ILogger<CandidatePhotoFormModel> logger) : base(mediator, logger)
        {
        }

        public int CandidateId { get; set; }

        public void OnGet(int candidateId)
        {
            CandidateId = candidateId;
        }

        public async Task<IActionResult> OnPost(IFormFile formFile, int candidateId)
        {
            await _mediator.Send(new CreateOrEditPhotoCommand(formFile, candidateId));
            return RedirectToPage("/User/Candidates/ConcreteCandidate", new { candidateId });
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class CandidatePhotoFormModel : BasePageModel
    {
        public CandidatePhotoFormModel(IMediator mediator, ILogger<CandidatePhotoFormModel> logger) : base(mediator, logger)
        {
        }

        public int? CandidateId { get; set; }
        public CandidateViewModel[] CandidateViewModels { get; set; }

        public void OnGet(int? candidateId)
        {
            CandidateId = candidateId;
        }

        public async Task<IActionResult> OnPostCreatePhoto(IFormFile formFile, int candidateId)
        {
            if(await _mediator.Send(new CreateOrChangePhotoCommand(formFile, candidateId)))
            {
                return RedirectToPage("/User/Candidates/ConcreteCandidate", new { candidateId });
            }
            ModelState.AddModelError("", "Не удалось проанализировать документ");
            CandidateId = candidateId;
            return Page();
        }

        public async Task OnPostSearchCandidates(string nameFragment)
        {
            var candidates = await _mediator.Send(new GetCandidatesByNameFragmentQuery(nameFragment));
            var config = new MapperConfiguration(c => c.CreateMap<Candidate, CandidateViewModel>());
            var mapper = new Mapper(config);
            CandidateViewModels = mapper.Map<CandidateViewModel[]>(candidates);
        }
    }
}

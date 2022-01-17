using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.PreviousJobs
{
    public class CreatePreviousJobPlacementModel : BasePageModel
    {
        public CreatePreviousJobPlacementModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public PreviousJobPlacementViewModel PreviousJobPlacementViewModel { get; set; }

        public void OnGet(int candidateId)
        {
            PreviousJobPlacementViewModel = new()
            {
                CandidateId = candidateId,
            };
        }

        public async Task<IActionResult> OnPost(PreviousJobPlacementViewModel previousJobPlacementViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(
                    c => c.CreateMap<PreviousJobPlacementViewModel, PreviousJobPlacement>());
                var mapper = new Mapper(config);
                var previousJobPlacementEntity = mapper.Map<PreviousJobPlacement>(previousJobPlacementViewModel);
                await _mediator.Send(new CreateEntityCommand<PreviousJobPlacement>(previousJobPlacementEntity));
                return RedirectToPage("PreviousJobs",
                    new { candidateId = previousJobPlacementViewModel.CandidateId });
            }
            PreviousJobPlacementViewModel = previousJobPlacementViewModel;
            return Page();
        }
    }
}

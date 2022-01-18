using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.EducationOfCandidate
{
    public class CreateEducationModel : BasePageModel
    {
        public CreateEducationModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public EducationViewModel EducationViewModel { get; set; }

        public void OnGet(int candidateId)
        {
            EducationViewModel = new()
            {
                CandidateId = candidateId
            };
        }

        public async Task<IActionResult> OnPost(EducationViewModel educationViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<EducationViewModel, Education>());
                var mapper = new Mapper(config);
                var educationEntity = mapper.Map<Education>(educationViewModel);
                await _mediator.Send(new CreateEntityCommand<Education>(educationEntity));
                return RedirectToPage("EducationOfCandidate", new { candidateId = educationViewModel.CandidateId });
            }
            EducationViewModel = educationViewModel;
            return Page();
        }
    }
}

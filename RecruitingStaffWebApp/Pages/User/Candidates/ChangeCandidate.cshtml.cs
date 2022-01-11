using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class ChangeCandidateModel : BasePageModel
    {
        public ChangeCandidateModel(IMediator mediator, ILogger<ChangeCandidateModel> logger) : base(mediator, logger)
        { }

        public CandidateViewModel CandidateViewModel { get; set; }

        public async Task OnGet(int candidateId)
        {
            await Initialize(candidateId);
        }

        public async Task<IActionResult> OnPost(CandidateViewModel candidateViewModel, int questionnaireId, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<CandidateViewModel, Candidate>());
                var mapper = new Mapper(config);
                var candidateEntity = mapper.Map<Candidate>(candidateViewModel);
                await _mediator.Send(new ChangeEntityCommand<Candidate>(candidateEntity));
                foreach (var vacancyId in candidateViewModel.VacancyIds)
                {
                    await _mediator.Send(new CreateOrChangeMapCommand<CandidateVacancy>(candidateEntity.Id, vacancyId));
                }
                return RedirectToPage("ConcreteCandidate", new { CandidateId = candidateViewModel.Id });
            }
            await Initialize(candidateViewModel.Id);
            return Page();
        }

        private async Task Initialize(int candidateId)
        {
            var candidateEntity = await _mediator.Send(new GetEntityByIdQuery<Candidate>(candidateId));
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Candidate, CandidateViewModel>());
            var mapper = new Mapper(config);
            CandidateViewModel = mapper.Map<CandidateViewModel>(candidateEntity);
            var vacanciesSelectList = new SelectList(
                await _mediator.Send(new GetEntitiesQuery<Vacancy>()),
                "Id",
                "Name");
            CandidateViewModel.VacanciesSelectList = vacanciesSelectList;
        }
    }
}

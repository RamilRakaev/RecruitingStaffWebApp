using AutoMapper;
using MediatR;
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
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class CreateCandidateModel : BasePageModel
    {
        public CreateCandidateModel(IMediator mediator, ILogger<CreateCandidateModel> logger) : base(mediator, logger)
        {
        }

        public CandidateViewModel CandidateViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var vacanciesSelectList = new SelectList(
                await _mediator.Send(new GetEntitiesQuery<Vacancy>()),
                "Id",
                "Name");
            if (vacanciesSelectList.Any() == false)
            {
                return RedirectToPage("Candidates", new { messageAboutDocumentsSource = "?? ??????? ????????" });
            }
            CandidateViewModel = new(vacanciesSelectList);
            return Page();
        }

        public async Task<IActionResult> OnPost(CandidateViewModel candidateViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<CandidateViewModel, Candidate>());
                var mapper = new Mapper(config);
                var candidateEntity = mapper.Map<Candidate>(candidateViewModel);
                await _mediator.Send(new CreateEntityCommand<Candidate>(candidateEntity));
                foreach (var vacancyId in candidateViewModel.VacancyIds)
                {
                    await _mediator.Send(new CreateMapCommand<CandidateVacancy>(candidateEntity.Id, vacancyId));
                }
                return RedirectToPage("Candidates");
            }
            var vacanciesSelectList = new SelectList(
                await _mediator.Send(
                    new GetEntitiesQuery<Vacancy>()),
                "Id",
                "Name");
            CandidateViewModel = candidateViewModel;
            CandidateViewModel.VacanciesSelectList = vacanciesSelectList;
            return Page();
        }
    }
}

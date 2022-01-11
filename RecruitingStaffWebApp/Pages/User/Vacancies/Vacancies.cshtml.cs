using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Vacancies
{
    public class VacanciesModel : BasePageModel
    {
        public VacanciesModel(IMediator mediator, ILogger<VacanciesModel> logger) : base(mediator, logger)
        {
        }

        public VacancyViewModel[] Vacancies { get; set; }

        public async Task OnGet()
        {
            _logger.LogInformation("\"Vacancies\" page visited");
            var vacancyEntities = await _mediator.Send(new GetEntitiesQuery<Vacancy>());
            var config = new MapperConfiguration(c => c.CreateMap<Vacancy, VacancyViewModel>());
            var mapper = new Mapper(config);
            Vacancies = mapper.Map<VacancyViewModel[]>(vacancyEntities);
        }

        public async Task<IActionResult> OnPost(int vacancyId)
        {
            await _mediator.Send(new RemoveVacancyCommand(vacancyId));
            RemoveLog("Vacancy", vacancyId);
            return RedirectToPage("Vacancies");
        }
    }
}

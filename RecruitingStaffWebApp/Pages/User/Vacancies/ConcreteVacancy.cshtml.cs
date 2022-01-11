using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Vacancies
{
    public class ConcreteVacancyModel : BasePageModel
    {
        public ConcreteVacancyModel(IMediator mediator, ILogger<ConcreteVacancyModel> logger) : base(mediator, logger)
        {
        }

        public VacancyViewModel VacancyViewModel { get; set; }

        public async Task OnGet(int vacancyId)
        {
            var vacancyEntity = await _mediator.Send(new GetEntityByIdQuery<Vacancy>(vacancyId));
            var config = new MapperConfiguration(c => c.CreateMap<Vacancy, VacancyViewModel>());
            var mapper = new Mapper(config);
            VacancyViewModel = mapper.Map<VacancyViewModel>(vacancyEntity);
        }

        public async Task<IActionResult> OnPost(int vacancyId)
        {
            await _mediator.Send(new RemoveVacancyCommand(vacancyId));
            return RedirectToPage("Vacancies");
        }
    }
}

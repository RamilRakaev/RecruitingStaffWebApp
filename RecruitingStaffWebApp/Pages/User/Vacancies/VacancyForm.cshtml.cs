using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Vacancies
{
    public class VacancyFormModel : BasePageModel
    {
        public VacancyFormModel(IMediator mediator, ILogger<VacancyFormModel> logger) : base(mediator, logger)
        { }

        public VacancyViewModel VacancyViewModel { get; set; }

        public async Task OnGet(int? vacancyId)
        {
            if (vacancyId == null)
            {
                VacancyViewModel = new VacancyViewModel();
                _logger.LogInformation("\"VacancyForm\" page has been visited to create vacancy");
            }
            else
            {
                var vacancyEntity = await _mediator.Send(new GetEntityByIdQuery<Vacancy>(vacancyId.Value));
                var config = new MapperConfiguration(c => c.CreateMap<Vacancy, VacancyViewModel>());
                var mapper = new Mapper(config);
                VacancyViewModel = mapper.Map<VacancyViewModel>(vacancyEntity);
                _logger.LogInformation("\"VacancyForm\" page has been visited to change vacancy");
            }
        }

        public async Task<IActionResult> OnPost(VacancyViewModel vacancyViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<VacancyViewModel, Vacancy>());
                var mapper = new Mapper(config);
                var vacancyEntity = mapper.Map<Vacancy>(vacancyViewModel);
                await _mediator.Send(new CreateOrChangeEntityCommand<Vacancy>(vacancyEntity));
                _logger.LogInformation("The vacancy has been created.");
                return RedirectToPage("/User/Vacancies/Vacancies");
            }
            _logger.LogInformation("Data entered incorrectly");
            ModelState.AddModelError("", "Неправильно введены данные");
            VacancyViewModel = vacancyViewModel;
            return Page();
        }
    }
}

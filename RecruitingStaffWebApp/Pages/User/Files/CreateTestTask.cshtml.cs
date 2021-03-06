using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Files;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class CreateTestTaskModel : BasePageModel
    {
        public CreateTestTaskModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public TestTaskViewModel TestTaskViewModel { get; set; }

        public async Task OnGet()
        {
            TestTaskViewModel = new();
            TestTaskViewModel.VacanciesSelectList = new SelectList(await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
        }

        public async Task<IActionResult> OnPost(IFormFile formFile, TestTaskViewModel testTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<TestTaskViewModel, TestTask>());
                var mapper = new Mapper(config);
                var testTaskEntity = mapper.Map<TestTask>(testTaskViewModel);
                await _mediator.Send(new CreateOrChangeEntityCommand<TestTask>(testTaskEntity));
                await _mediator.Send(new CreateOrChangeFileCommand(formFile, testTaskEntity.Name, FileType.TestTask, testTaskId: testTaskEntity.Id));
                return RedirectToPage("Files");
            }
            TestTaskViewModel = new();
            TestTaskViewModel.VacanciesSelectList = new SelectList(await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
            return Page();
        }
    }
}

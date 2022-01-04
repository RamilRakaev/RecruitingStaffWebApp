using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Files;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class TestTaskFormModel : BasePageModel
    {
        public TestTaskFormModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public TestTaskViewModel TestTaskViewModel { get; set; }
        public SelectList Vacancies { get; set;
        }
        public async Task OnGet()
        {
            TestTaskViewModel = new();
            Vacancies = new SelectList(await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
        }

        public async Task<IActionResult> OnPost(IFormFile formFile, TestTaskViewModel testTaskViewModel)
        {
            var testTaskEntity = GetEntity<TestTask, TestTaskViewModel>(testTaskViewModel);
            await _mediator.Send(new CreateOrChangeFileCommand(formFile, FileType.TestTask, testTaskId: testTaskEntity.Id));
            return RedirectToPage("Files");
        }
    }
}

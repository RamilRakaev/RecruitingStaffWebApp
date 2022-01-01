using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.File;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class FilesModel : BasePageModel
    {
        public FilesModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public FileViewModel[] Files { get; set; }

        public async Task OnGet()
        {
            var files = await _mediator.Send(new GetEntitiesQuery<RecruitingStaffWebAppFile>());
            Files = GetViewModels<RecruitingStaffWebAppFile, FileViewModel>(files);
        }
    }
}

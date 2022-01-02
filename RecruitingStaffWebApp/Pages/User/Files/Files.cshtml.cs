using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
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
        public FileViewModel[] EmptyQuestionnaires { get; set; }
        public FileViewModel[] CompletedQuestionnaires { get; set; }
        public FileViewModel[] TestTasks { get; set; }
        public FileViewModel[] Photos { get; set; }
        public FileViewModelType FileViewModelType { get; set; }
        public SelectList FileTypes { get; set; }
        public int SelectedFileType;

        public async Task OnGet()
        {
            await Initial();
        }

        public async Task OnPost(int fileType)
        {
            SelectedFileType = fileType;
            await Initial();
        }

        private async Task Initial()
        {
            FileTypes = new(await _mediator.Send(new GetValuesQuery(typeof(FileType))), "Key", "Value");
            Files = await GetViewModelsByFileType((FileType)SelectedFileType);
        }

        private async Task<FileViewModel[]> GetViewModelsByFileType(FileType fileType)
        {
            var files = await _mediator.Send(new GetFilesByTypeQuery(fileType));
            return GetViewModels<RecruitingStaffWebAppFile, FileViewModel>(files);
        }
    }
}

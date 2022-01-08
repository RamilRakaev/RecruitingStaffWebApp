using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using RecruitingStaff.WebApp.ViewModels.Files;
using RecruitingStaffWebApp.Pages.User;
using System.IO;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class FilesModel : BasePageModel
    {
        public FilesModel(IMediator mediator, ILogger<BasePageModel> logger, IOptions<WebAppOptions> options) : base(mediator, logger)
        {
            _options = options.Value;
        }

        private WebAppOptions _options;
        public FileViewModel[] Files { get; set; }
        public SelectList FileTypes { get; set; }
        public int SelectedFileType;

        public async Task OnGet()
        {
            await Initial();
        }

        public async Task<IActionResult> OnGetGetFile(int fileId, int fileType)
        {
            SelectedFileType = fileType;
            await Initial();
            var file = await _mediator.Send(new GetEntityByIdQuery<RecruitingStaffWebAppFile>(fileId));

            string file_path = Path.Combine(_options.GetSource((FileType) SelectedFileType), file.Name);
            // Тип файла - content-type
            string file_type = "application/vnd.openxmlformants-officedocument.wordprocessingml.document";
            // Имя файла - необязательно
            string file_name = "document.docx";
            return PhysicalFile(file_path, file_type, file_name);

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

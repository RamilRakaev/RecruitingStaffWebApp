using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
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
        public FilesModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

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
            string source = await _mediator.Send(new GetFileSourceQuery(SelectedFileType));
            string file_path = Path.Combine(source, file.Name);
            string file_type = "application/vnd.openxmlformants-officedocument.wordprocessingml.document";
            return PhysicalFile(file_path, file_type, file.Name);
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
            var config = new MapperConfiguration(
                c => c.CreateMap<RecruitingStaffWebAppFile, FileViewModel>());
            var mapper = new Mapper(config);
            return mapper.Map<FileViewModel[]>(files);
        }
    }
}

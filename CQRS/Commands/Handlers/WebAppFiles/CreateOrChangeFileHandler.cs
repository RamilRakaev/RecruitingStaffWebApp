using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.WebAppFiles
{
    public class CreateOrChangeFileHandler : IRequestHandler<CreateOrChangeFileCommand, RecruitingStaffWebAppFile>
    {
        private readonly IMediator _mediator;
        private readonly WebAppOptions _options;

        public CreateOrChangeFileHandler(IMediator mediator, IOptions<WebAppOptions> options)
        {
            _mediator = mediator;
            _options = options.Value;
        }

        public async Task<RecruitingStaffWebAppFile> Handle(CreateOrChangeFileCommand request, CancellationToken cancellationToken)
        {
            var fileEntity = await _mediator.Send(
                new CreateOrChangeEntityCommand<RecruitingStaffWebAppFile>(request.FileEntity),
                cancellationToken);
            await request.FormFile.CreateNewFileAsync(
                _options.GetSource(fileEntity.FileType) + "\\" + fileEntity.Name);
            return fileEntity;
        }
    }
}

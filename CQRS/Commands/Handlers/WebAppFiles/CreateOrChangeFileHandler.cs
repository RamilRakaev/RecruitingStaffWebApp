using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.WebAppFiles
{
    public class CreateOrChangeFileHandler : IRequestHandler<CreateOrChangeFileCommand, RecruitingStaffWebAppFile>
    {
        private readonly IMediator _mediator;

        public CreateOrChangeFileHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RecruitingStaffWebAppFile> Handle(CreateOrChangeFileCommand request, CancellationToken cancellationToken)
        {
            var fileEntity = await _mediator.Send(
                new CreateOrChangeEntityCommand<RecruitingStaffWebAppFile>(request.FileEntity),
                cancellationToken);
            var source = await _mediator.Send(new GetFileSourceQuery(fileEntity.FileType), cancellationToken);
            await request.FormFile.CreateNewFileAsync(
                Path.Combine(source, fileEntity.Name + ".docx"));
            return fileEntity;
        }
    }
}

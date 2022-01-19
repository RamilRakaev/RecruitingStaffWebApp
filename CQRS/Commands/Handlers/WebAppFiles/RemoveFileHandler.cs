using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.WebAppFiles
{
    public class RemoveFileHandler : IRequestHandler<RemoveFileCommand, RecruitingStaffWebAppFile>
    {
        private readonly IMediator _mediator;
        private readonly WebAppOptions _options;

        public RemoveFileHandler(IMediator mediator, IOptions<WebAppOptions> options)
        {
            _mediator = mediator;
            _options = options.Value;
        }

        public async Task<RecruitingStaffWebAppFile> Handle(RemoveFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _mediator.Send(new RemoveEntityCommand<RecruitingStaffWebAppFile>(request.FileId), cancellationToken);
            File.Delete(Path.Combine(_options.GetSource(file.FileType), file.Name));
            return file;
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.WebAppFiles
{
    public class GetFileSourceHandler : IRequestHandler<GetFileSourceQuery, string>
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly WebAppOptions _options;

        public GetFileSourceHandler(IWebHostEnvironment appEnvironment, IOptions<WebAppOptions> options)
        {
            _appEnvironment = appEnvironment;
            _options = options.Value;
        }

        public Task<string> Handle(GetFileSourceQuery request, CancellationToken cancellationToken)
        {

            return Task.FromResult(request.FileType switch
            {
                FileType.CompletedQuestionnaire => _options.CandidateDocumentsSource,
                FileType.QuestionnaireExample => _options.EmptyQuestionnairesSource,
                FileType.TestTask => _options.TestTasksSource,
                FileType.Photo => Path.Combine(_appEnvironment.WebRootPath, "img"),
                _ => string.Empty,
            });
        }
    }
}

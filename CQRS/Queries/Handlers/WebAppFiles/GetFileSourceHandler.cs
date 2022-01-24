using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.WebAppFiles
{
    public class GetFileSourceHandler : IRequestHandler<GetFileSourceQuery, string>
    {
        private readonly WebAppOptions _options;

        public GetFileSourceHandler(IOptions<WebAppOptions> options)
        {
            _options = options.Value;
        }

        public Task<string> Handle(GetFileSourceQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.FileType switch
            {
                FileType.CompletedQuestionnaire => _options.CompletedQuestionnaireSource,
                FileType.QuestionnaireExample => _options.QuestionnaireExampleSource,
                FileType.TestTask => _options.TestTasksSource,
                FileType.JpgPhoto => _options.JpgPhotosSource,
                _ => string.Empty,
            });
        }
    }
}

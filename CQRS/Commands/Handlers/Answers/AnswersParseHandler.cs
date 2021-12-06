using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Answers
{
    public class AnswersParseHandler : IRequestHandler<AnswersParseCommand, bool>
    {
        private readonly IQuestionnaireManager _questionnaireManager;
        private readonly WebAppOptions _options;

        public AnswersParseHandler(IQuestionnaireManager questionnaireManager, IOptions<WebAppOptions> options)
        {
            _questionnaireManager = questionnaireManager;
            _options = options.Value;
        }

        public Task<bool> Handle(AnswersParseCommand request, CancellationToken cancellationToken)
        {
            if (request.FormFile != null)
            {
                var guid = Guid.NewGuid();
                using (var stream = new FileStream($"{_options.DocumentsSource}\\{guid}", FileMode.CreateNew))
                {
                    request.FormFile.CopyTo(stream);
                }
                return _questionnaireManager.Parse(
                    guid.ToString(),
                    (JobQuestionnaire)request.JobQuestionnaire,
                    false);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}

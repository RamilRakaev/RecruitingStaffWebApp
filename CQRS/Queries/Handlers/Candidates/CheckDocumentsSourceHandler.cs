using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Candidates
{
    public class CheckDocumentsSourceHandler : IRequestHandler<CheckDocumentsSourceCommand, string>
    {
        private readonly WebAppOptions _options;

        public CheckDocumentsSourceHandler(IOptions<WebAppOptions> options)
        {
            _options = options.Value;
        }

        public Task<string> Handle(CheckDocumentsSourceCommand request, CancellationToken cancellationToken)
        {
            if (_options.CandidateDocumentsSource == null)
            {
                return Task.FromResult("Расположение документов не указано");
            }
            string path = _options.CandidateDocumentsSource;
            if (Directory.Exists(path) == false)
            {
                return Task.FromResult("Указанной папки не существует");
            }
            try
            {
                using (var file = new FileStream(path + "\\file", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (!file.CanWrite || !file.CanRead)
                    {
                        return Task.FromResult("Доступ к указанному расположению документов запрещён.");
                    }
                }
                File.Delete(path + "\\file");
            }
            catch (Exception e)
            {
                return Task.FromResult("Доступ к указанному расположению документов запрещён.\n" +
                    $"{e.Message}");
            }
            return Task.FromResult("");
        }
    }
}

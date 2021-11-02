using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Domain.Model;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Options;

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
            if (_options.DocumentsSource == null)
            {
                return Task.FromResult("Расположение документов не указано");
            }
            string path = _options.DocumentsSource;
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

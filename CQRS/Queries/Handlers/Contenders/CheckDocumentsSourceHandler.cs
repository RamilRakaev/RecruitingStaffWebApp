using CQRS.Queries.Requests.Contenders;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CQRS.Queries.Handlers.Contenders
{
    public class CheckDocumentsSourceHandler : IRequestHandler<CheckDocumentsSourceCommand, string>
    {
        private readonly IRepository<Option> _optionRepository;

        public CheckDocumentsSourceHandler(IRepository<Option> optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task<string> Handle(CheckDocumentsSourceCommand request, CancellationToken cancellationToken)
        {
            var documentsSource = await _optionRepository
                .GetAllAsNoTracking()
                .FirstOrDefaultAsync(
                o =>
                o.PropertyName == OptionTypes.DocumentsSource,
                cancellationToken: cancellationToken);
            if (documentsSource == null)
            {
                return "Расположение документов не указано";
            }
            string path = documentsSource.Value;
            if (Directory.Exists(path) == false)
            {
                return "Указанной папки не существует";
            }
            try
            {
                using (var file = new FileStream(path + "\\file", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (!file.CanWrite || !file.CanRead)
                    {
                        return "Доступ к указанному расположению документов запрещён.";
                    }
                }
                File.Delete(path + "\\file");
            }
            catch (Exception e)
            {
                return "Доступ к указанному расположению документов запрещён.\n" +
                    $"{e.Message}";
            }
            return "";
        }
    }
}

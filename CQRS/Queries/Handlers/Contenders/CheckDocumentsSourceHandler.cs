using CQRS.Queries.Requests.Contenders;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Linq;
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
                File.Create(path + "\\file");
                File.Delete(path + "\\file");
            }
            catch(Exception e)
            {
                return "Доступ к указанному расположению документов запрещён.\n" +
                    $"{e.Message}";
            }
            return "";
        }
    }
}

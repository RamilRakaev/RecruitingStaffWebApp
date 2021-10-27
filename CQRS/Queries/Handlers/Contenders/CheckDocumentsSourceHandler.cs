using CQRS.Queries.Requests.Contenders;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
            string path = "";
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
            if(File.Exists(path) == false)
            {
                return "Указанной папки не существует";
            }
            return "Все настройки введены";
        }
    }
}

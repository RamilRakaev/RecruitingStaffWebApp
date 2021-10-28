using CQRS.Commands.Requests.Contenders;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commands.Handlers.Contenders
{
    public class CreateContenderHandler : IRequestHandler<CreateContenderCommand, bool>
    {
        private readonly IRepository<Contender> _contenderRepository;
        private readonly IRepository<Option> _optionRepository;

        public CreateContenderHandler(IRepository<Contender> contenderRepository, IRepository<Option> optionRepository)
        {
            _contenderRepository = contenderRepository;
            _optionRepository = optionRepository;
        }

        public async Task<bool> Handle(CreateContenderCommand request, CancellationToken cancellationToken)
        {
            var documentSource = _optionRepository.GetAllAsNoTracking().FirstOrDefault(o => o.PropertyName == OptionTypes.DocumentsSource);
            if (documentSource != null)
            {
                await _contenderRepository.AddAsync(request.Contender);
                await _contenderRepository.SaveAsync();
                var contender = _contenderRepository.GetAll().FirstOrDefault(c => c == request.Contender);
                string path = $"{documentSource.Value}\\{contender.DocumentSource}";
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await request.UploadedFile.CopyToAsync(fileStream, cancellationToken);
                }
                await _contenderRepository.SaveAsync();
                return true;
            }
            return false;
        }
    }
}

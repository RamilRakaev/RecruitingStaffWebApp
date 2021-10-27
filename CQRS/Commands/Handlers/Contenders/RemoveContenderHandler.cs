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
    public class RemoveContenderHandler : IRequestHandler<RemoveContenderCommand, bool>
    {
        private readonly IRepository<Contender> _contenderRepository;
        private readonly IRepository<Option> _optionRepository;

        public RemoveContenderHandler(IRepository<Contender> contenderRepository, IRepository<Option> optionRepository)
        {
            _contenderRepository = contenderRepository;
            _optionRepository = optionRepository;
        }

        public async Task<bool> Handle(RemoveContenderCommand request, CancellationToken cancellationToken)
        {
            var contender = await _contenderRepository.FindAsync(request.Id);
            if (contender != null)
            {
                var documentSource = _optionRepository
                    .GetAllAsNoTracking()
                    .FirstOrDefault(
                    o => o.PropertyName == OptionTypes.DocumentsSource);

                if (documentSource != null)
                {
                    string path = documentSource.Value + "\\" + contender.DocumentSource;
                    File.Delete(path);
                    await _contenderRepository.RemoveAsync(contender);
                    await _contenderRepository.SaveAsync();
                    return true;
                }
            }
            return false;
        }
    }
}

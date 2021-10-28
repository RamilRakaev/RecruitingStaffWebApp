using CQRS.Commands.Requests.Contenders;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commands.Handlers.Contenders
{
    public class ChangeContenderHandler : IRequestHandler<ChangeContenderCommand, bool>
    {
        private readonly IRepository<Contender> _contenderRepository;
        private readonly IRepository<Option> _optionRepository;

        public ChangeContenderHandler(IRepository<Contender> contenderRepository, IRepository<Option> optionRepository)
        {
            _contenderRepository = contenderRepository;
            _optionRepository = optionRepository;
        }

        public async Task<bool> Handle(ChangeContenderCommand request, CancellationToken cancellationToken)
        {
            var contender = await _contenderRepository.FindAsync(request.Contender.Id);
            var documentSource = await _optionRepository.GetAllAsNoTracking().FirstOrDefaultAsync(o => o.PropertyName == OptionTypes.DocumentsSource);
            if(documentSource != null)
            {
                var file = new FileInfo($"{documentSource.Value}\\{contender.DocumentSource}");

                contender.FullName = request.Contender.FullName;
                contender.Address = request.Contender.Address;
                contender.DateOfBirth = request.Contender.DateOfBirth;
                await _contenderRepository.SaveAsync();

                if (file.Exists)
                {
                    file.MoveTo($"{documentSource.Value}\\{contender.DocumentSource}");
                }
                return true;
            }
            return false;
        }
    }
}

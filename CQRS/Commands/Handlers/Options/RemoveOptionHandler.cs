using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Options
{
    public class RemoveOptionHandler : IRequestHandler<RemoveOptionCommand, Option>
    {
        private readonly IRepository<Option> _optionRepository;

        public RemoveOptionHandler(IRepository<Option> optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task<Option> Handle(RemoveOptionCommand request, CancellationToken cancellationToken)
        {
            var option = await _optionRepository.FindAsync(request.OptionId);
            await _optionRepository.RemoveAsync(option);
            await _optionRepository.SaveAsync();
            return option;
        }
    }
}

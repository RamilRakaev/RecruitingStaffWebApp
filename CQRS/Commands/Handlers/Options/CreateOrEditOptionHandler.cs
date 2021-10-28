using CQRS.Commands.Requests.Options;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commands.Handlers.Options
{
    public class CreateOrEditOptionHandler : IRequestHandler<CreateOrEditOptionCommand, Option>
    {
        private readonly IRepository<Option> _optionRepository;

        public CreateOrEditOptionHandler(IRepository<Option> optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task<Option> Handle(CreateOrEditOptionCommand request, CancellationToken cancellationToken)
        {
            var option = await _optionRepository
                .GetAll()
                .FirstOrDefaultAsync(o => o.PropertyName == request.Option.PropertyName && o.ContenderId == request.Option.ContenderId);
            if(option == null)
            {
                await _optionRepository.AddAsync(request.Option);
            }
            else
            {
                option.Value = request.Option.Value;
                option.ContenderId = request.Option.ContenderId;
            }
            await _optionRepository.SaveAsync();
            return request.Option;
        }
    }
}

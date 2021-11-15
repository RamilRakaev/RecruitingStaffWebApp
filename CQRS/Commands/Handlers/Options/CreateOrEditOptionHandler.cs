using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Options
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
                .FirstOrDefaultAsync(
                o => o.PropertyName == request.Option.PropertyName &&
                o.CandidateId == request.Option.CandidateId, 
                cancellationToken: cancellationToken);

            if(option == null)
            {
                await _optionRepository.AddAsync(request.Option, cancellationToken);
            }
            else
            {
                option.Value = request.Option.Value;
                option.CandidateId = request.Option.CandidateId;
            }
            await _optionRepository.SaveAsync(cancellationToken);
            return request.Option;
        }
    }
}

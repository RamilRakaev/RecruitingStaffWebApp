using RecruitingStaff.Domain.Model;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options
{
    public class CreateOrEditOptionCommand : IRequest<Option>
    {
        public CreateOrEditOptionCommand(Option option)
        {
            Option = option;
        }

        public Option Option { get; set; }
    }
}

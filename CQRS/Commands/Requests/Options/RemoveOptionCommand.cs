using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options
{
    public class RemoveOptionCommand : IRequest<Option>
    {
        public RemoveOptionCommand(int optionId)
        {
            OptionId = optionId;
        }

        public int OptionId { get; set; }
    }
}

using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class CreateMapCommand<TMap> : IRequest<TMap>
    {
        public CreateMapCommand(int firstEntityId, int secondEntityId)
        {
            FirstEntityId = firstEntityId;
            SecondEntityId = secondEntityId;
        }

        public int FirstEntityId { get; set; }
        public int SecondEntityId { get; set; }
    }
}

using MediatR;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class CreateOrChangeMapCommand<TMap> : IRequest<TMap>
        where TMap : BaseMap, new()
    {
        public CreateOrChangeMapCommand(int firstEntityId, int secondEntityId)
        {
            Map = new();
            Map.FirstEntityId = firstEntityId;
            Map.SecondEntityId = secondEntityId;
        }

        public TMap Map { get; set; }
    }
}

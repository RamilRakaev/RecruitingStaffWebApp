using MediatR;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps
{
    public class CreateMapCommand<TMap> : IRequest<TMap>
        where TMap : BaseMap, new()
    {
        public CreateMapCommand(int firstEntityId, int secondEntityId)
        {
            Map = new();
            Map.FirstEntityId = firstEntityId;
            Map.SecondEntityId = secondEntityId;
        }

        public TMap Map { get; set; }
    }
}

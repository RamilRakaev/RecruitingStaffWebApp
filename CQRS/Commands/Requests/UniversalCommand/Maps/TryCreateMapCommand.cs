using MediatR;
using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps
{
    public class TryCreateMapCommand<TMap> : IRequest<TMap>
        where TMap : BaseMap, new()
    {
        public TryCreateMapCommand(int firstEntityId, int secondEntityId)
        {
            Map = new();
            Map.FirstEntityId = firstEntityId;
            Map.SecondEntityId = secondEntityId;
        }

        public TMap Map { get; set; }
    }
}

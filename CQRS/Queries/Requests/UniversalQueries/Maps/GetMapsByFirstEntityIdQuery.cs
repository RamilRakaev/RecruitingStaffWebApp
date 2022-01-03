using MediatR;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries.Maps
{
    public class GetMapsByFirstEntityIdQuery<TMap> : IRequest<TMap[]>
        where TMap : BaseMap
    {
        public GetMapsByFirstEntityIdQuery(int firstEntityId)
        {
            FirstEntityId = firstEntityId;
        }

        public int FirstEntityId { get; set; }
    }
}

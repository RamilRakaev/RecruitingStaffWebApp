using MediatR;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries
{
    public class GetEntityByNameQuery<TEntity> : IRequest<TEntity>
        where TEntity : CandidatesSelectionEntity 
    {
        public GetEntityByNameQuery(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

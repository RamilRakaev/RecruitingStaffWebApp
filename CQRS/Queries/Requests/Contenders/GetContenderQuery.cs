using Domain.Model;
using MediatR;

namespace CQRS.Queries.Requests.Contenders
{
    public class GetContenderQuery : IRequest<Contender>
    {
        public GetContenderQuery(int id)
        {
            ContenderId = id;
        }

        public int ContenderId { get; set; }
    }
}

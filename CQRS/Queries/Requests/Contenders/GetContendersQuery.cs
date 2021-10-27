using Domain.Model;
using MediatR;

namespace CQRS.Queries.Requests.Contenders
{
    public class GetContendersQuery : IRequest<Contender[]>
    {
    }
}

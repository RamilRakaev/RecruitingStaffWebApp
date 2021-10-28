using MediatR;

namespace CQRS.Queries.Requests.Candidates
{
    public class CheckDocumentsSourceCommand : IRequest<string>
    {
    }
}

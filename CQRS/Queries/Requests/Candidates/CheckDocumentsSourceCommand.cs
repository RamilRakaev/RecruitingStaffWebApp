using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates
{
    public class CheckDocumentsSourceCommand : IRequest<string>
    {
    }
}

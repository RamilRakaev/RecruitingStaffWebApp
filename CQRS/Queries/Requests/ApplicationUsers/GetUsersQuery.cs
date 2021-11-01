using Domain.Model;
using Domain.Model.UserIdentity;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Request.ApplicationUsers
{
    public class GetUsersQuery : IRequest<ApplicationUser[]>
    {
    }
}

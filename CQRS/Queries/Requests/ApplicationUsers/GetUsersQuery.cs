using RecruitingStaff.Domain.Model.BaseEntities;
using RecruitingStaff.Domain.Model.UserIdentity;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Request.ApplicationUsers
{
    public class GetUsersQuery : IRequest<ApplicationUser[]>
    {
    }
}

using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.ApplicationUsers
{
    public class CheckRoleForUserQuery : IRequest<bool>
    {
        public CheckRoleForUserQuery(params string[] roles)
        {
            Roles = roles;
        }
        public string[] Roles { get; set; }
    }
}

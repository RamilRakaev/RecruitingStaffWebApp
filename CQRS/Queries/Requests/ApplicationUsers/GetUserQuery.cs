using Domain.Model.UserIdentity;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Request.ApplicationUsers
{
    public class GetUserQuery : IRequest<ApplicationUser>
    {
        public GetUserQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

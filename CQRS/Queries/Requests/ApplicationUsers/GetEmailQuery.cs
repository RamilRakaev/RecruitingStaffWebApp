using MediatR;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.ApplicationUsers
{
    public class GetEmailQuery : IRequest<string>
    {

        public GetEmailQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}

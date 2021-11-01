using Domain.Model.UserIdentity;
using MediatR;

namespace  RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers
{
    public class ChangePasswordCommand : IRequest<ApplicationUser>
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
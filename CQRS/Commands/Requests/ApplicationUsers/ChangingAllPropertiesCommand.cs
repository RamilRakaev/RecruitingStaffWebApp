using RecruitingStaff.Domain.Model.UserIdentity;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers
{
    public class ChangingAllPropertiesCommand : IRequest<ApplicationUser>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}

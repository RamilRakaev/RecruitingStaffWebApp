using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.ApplicationUsers
{
    public class ChangingAllUserPropertiesHandler : UserHandler, IRequestHandler<ChangingAllPropertiesCommand, ApplicationUser>
    {
        public ChangingAllUserPropertiesHandler(UserManager<ApplicationUser> db) : base(db)
        { }

        public async Task<ApplicationUser> Handle(ChangingAllPropertiesCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken: cancellationToken);
            user.UserName = command.Email;
            user.Email = command.Email;
            return user;
        }
    }
}

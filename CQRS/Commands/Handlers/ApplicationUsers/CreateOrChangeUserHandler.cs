using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.ApplicationUsers
{
    public class CreateOrChangeUserHandler : IRequestHandler<CreateOrChangeUserCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateOrChangeUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(CreateOrChangeUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            IdentityResult result;
            if (user == null)
            {
                var password = request.Password;
                var newUser = new ApplicationUser() { UserName = request.Email, Email = request.Email};
                result = await _userManager.CreateAsync(newUser, password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, request.Role);
                    await _userManager.UpdateAsync(newUser);
                }
            }
            else
            {
                user.Email = request.Email;
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, request.Password);
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRoleAsync(user, request.Role);
                result = await _userManager.UpdateAsync(user);
            }
            return result;
        }
    }
}

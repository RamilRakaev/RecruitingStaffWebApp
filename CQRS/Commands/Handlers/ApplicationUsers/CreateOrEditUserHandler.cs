using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.ApplicationUsers
{
    public class CreateOrEditUserHandler : IRequestHandler<CreateOrChangeUserCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateOrEditUserHandler(UserManager<ApplicationUser> userManager)
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
            }
            else
            {
                user.Email = request.Email;
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, request.Password);
                result = await _userManager.UpdateAsync(user);
            }
            return result;
        }
    }
}

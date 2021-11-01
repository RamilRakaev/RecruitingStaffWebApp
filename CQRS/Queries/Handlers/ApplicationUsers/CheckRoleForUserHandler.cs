using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.ApplicationUsers;
using Domain.Model.UserIdentity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.ApplicationUsers
{
    public class CheckRoleForUserHandler : IRequestHandler<CheckRoleForUserQuery, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public CheckRoleForUserHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(CheckRoleForUserQuery request, CancellationToken cancellationToken)
        {
            var id = _signInManager.Context.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);
            if(id != null)
            {
                var user = await _userManager.FindByIdAsync(id.Value);
                var roles = await _userManager.GetRolesAsync(user);
                return roles.Intersect(request.Roles).Any();
            }
            return false;
        }
    }
}

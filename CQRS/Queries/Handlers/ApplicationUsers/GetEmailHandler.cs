using MediatR;
using Microsoft.AspNetCore.Identity;
using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.ApplicationUsers;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.ApplicationUsers
{
    public class GetEmailHandler : IRequestHandler<GetEmailQuery, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetEmailHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(GetEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            return user.Email;
        }
    }
}

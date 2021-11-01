using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Infrastructure.CQRS.Queries.Request.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.ApplicationUsers
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(UserManager<ApplicationUser>));
        }

        public async Task<ApplicationUser> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByIdAsync(request.Id.ToString());
        }
    }
}

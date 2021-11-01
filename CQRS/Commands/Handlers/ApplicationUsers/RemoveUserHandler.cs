using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.ApplicationUsers
{
    public class RemoveUserHandler : UserHandler, IRequestHandler<RemoveUserCommand, ApplicationUser>
    {
        public RemoveUserHandler(UserManager<ApplicationUser> db) : base(db)
        { }

        public async Task<ApplicationUser> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                throw new NullReferenceException();
            }
            await _userManager.DeleteAsync(user);
            return user;
        }
    }
}

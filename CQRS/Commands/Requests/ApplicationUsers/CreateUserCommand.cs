using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.CQRS.Commands.Requests.ApplicationUsers
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public CreateUserCommand(ApplicationUser user)
        {
            User = user;
        }

        public ApplicationUser User { get; set; }
    }
}

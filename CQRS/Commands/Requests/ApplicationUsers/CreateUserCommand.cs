using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.CQRS.Commands.Requests.ApplicationUsers
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public CreateUserCommand(string email, string password, int roleId)
        {
            Email = email ?? throw new ArgumentNullException();
            Password = password ?? throw new ArgumentNullException();
            RoleId = roleId;
        }

        public int RoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers
{
    public class CreateOrChangeUserCommand : IRequest<IdentityResult>
    {
        public CreateOrChangeUserCommand()
        {

        }

        public CreateOrChangeUserCommand(string email, string password, int roleId = 1, int id = 0)
        {
            Email = email ?? throw new ArgumentNullException();
            Password = password ?? throw new ArgumentNullException();
            RoleId = roleId;
            Id = id;
        }

        public int Id { get; set; }
        public int RoleId { get; set; } = 1;
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

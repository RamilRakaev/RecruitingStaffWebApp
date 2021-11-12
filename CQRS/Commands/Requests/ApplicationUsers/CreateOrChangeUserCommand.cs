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

        public CreateOrChangeUserCommand(string email, string password, string role = "user", int id = 0)
        {
            Email = email ?? throw new ArgumentNullException();
            Password = password ?? throw new ArgumentNullException();
            Role = role;
            Id = id;
        }

        public int Id { get; set; }
        public string Role { get; set; } = "user";
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

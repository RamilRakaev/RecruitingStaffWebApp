using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers
{
    public class CreateOrChangeUserCommand : IRequest<IdentityResult>
    {
        public CreateOrChangeUserCommand(string email, string password, int id = 0, string role = "user")
        {
            Email = email ?? throw new ArgumentNullException();
            Password = password ?? throw new ArgumentNullException();
            Id = id;
            Role = role;
        }

        public int Id { get; set; }
        public string Role { get; set; } = "user";
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

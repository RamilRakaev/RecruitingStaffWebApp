﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers
{
    public class CreateOrEditUserCommand : IRequest<IdentityResult>
    {
        public CreateOrEditUserCommand(string email, string password, int id = 0, int roleId = 1)
        {
            Email = email ?? throw new ArgumentNullException();
            Password = password ?? throw new ArgumentNullException();
            Id = id;
            RoleId = roleId;
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

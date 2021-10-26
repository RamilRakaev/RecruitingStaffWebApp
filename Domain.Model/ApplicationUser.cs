using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int RoleId { get; set; }
        public ApplicationUserRole Role { get; set; }
    }
}

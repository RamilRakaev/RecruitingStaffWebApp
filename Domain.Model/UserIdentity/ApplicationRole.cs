using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Model.UserIdentity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public List<ApplicationUser> Users { get; set; }
    }
}

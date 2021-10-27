using System.Threading.Tasks;
using CQRS.Queries.Requests.ApplicationUsers;
using CQRS.Queries.Requests.Contenders;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecruitingStaffWebApp.Pages.User
{
    public class ConcreteContenderModel : BaseContenderModel
    {
        public ConcreteContenderModel(IMediator mediator) : base(mediator)
        { }

        public Contender Contender { get; set; } 

        public async Task<IActionResult> OnGet(int contenderId)
        {
            return await RightVerification();
        }
    }
}

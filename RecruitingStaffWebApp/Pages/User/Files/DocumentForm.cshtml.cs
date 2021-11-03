using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaffWebApp.Pages.User;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class DocumentFormModel : BasePageModel
    {
        public DocumentFormModel(IMediator mediator) : base(mediator)
        { }

        public void OnGet()
        { }

        public async Task OnPost(IFormFile formFile)
        {
            await _mediator.Send(new DocumentParseCommand(formFile));
        }
    }
}

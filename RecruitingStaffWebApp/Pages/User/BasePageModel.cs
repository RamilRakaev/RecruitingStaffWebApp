using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RecruitingStaffWebApp.Pages.User
{
    [Authorize(Policy = "RequireUserRole")]
    public class BasePageModel : PageModel
    {
        protected readonly IMediator _mediator;
        protected readonly ILogger<BasePageModel> _logger;

        public BasePageModel(IMediator mediator, ILogger<BasePageModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        protected void RemoveLog(string entity, int id)
        {
            _logger.LogDebug($"The entity {entity} with id {id} has been removed.");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaffWebApp.Pages.Account;

namespace RecruitingStaffWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserProperties _userProperties;

        public IndexModel(ILogger<IndexModel> logger, UserProperties userProperties)
        {
            _logger = logger;
            _userProperties = userProperties;
        }

        public IActionResult OnGet()
        {
            _logger.LogInformation("Index page visited");
            if (_userProperties.RoleId != 1)
            {
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }
    }
}

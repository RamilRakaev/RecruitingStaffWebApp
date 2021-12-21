namespace RecruitingStaff.WebApp.ViewModels.ApplicationUser
{
    public class ApplicationUserViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Role { get; set; } = "user";
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

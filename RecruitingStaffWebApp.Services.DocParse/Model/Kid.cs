namespace RecruitingStaffWebApp.Services.DocParse.Model
{
    public class Kid
    {
        public string Name {
            get { 
                return $"{Gender} {Age}";
            }
        }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}

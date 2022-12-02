using System.ComponentModel.DataAnnotations;

namespace Study_Hours_App.Models
{
    public class SelfStudyHours
    {
        public int SelfStudyHoursId { get; set; }
        [Display(Name = "Module Code")]
        public string ModuleCode { get; set; }
        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }
        [Display(Name = "Module Credits")]
        public string ModuleCredits { get; set; }
        [Display(Name = "Total Self-Study Hours")]
        public string SelfStudyHour { get; set; }
        [Display(Name = "Period")]
        public string Semester { get; set; }
        [Display(Name = "Current Hour Spent")]
        public string NumOfHours { get; set; }
        [Display(Name = "Hours Left")]
        public string NumOfHoursLeft { get; set; }
        public string UserId { get; set; }
    }
}

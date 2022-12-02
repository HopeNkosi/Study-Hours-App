using System.ComponentModel.DataAnnotations;

namespace Study_Hours_App.Models
{
    public class ModulesDashboard
    {
        public int ModulesDashboardId { get; set; }
        [Display(Name = "Module Code")]
        public string ModuleCode { get; set; }
        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }
        [Display(Name = "Module Credits")]
        public int ModuleNumberOfCredits { get; set; }
        [Display(Name = "Total Hours for Module")]
        public int ClassHoursPerWeek { get; set; }
        [Display(Name = "Self Study Hours")]
        public double MySelfStudy { get; set; }
        [Display(Name = "Period")]
        public int SemesterDashbaordId { get; set; }
        public virtual SemesterDashbaord SemesterDashbaord { get; set; }
        public string UserId {  get; set; }
    }
}

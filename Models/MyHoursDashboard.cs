using System;
using System.ComponentModel.DataAnnotations;

namespace Study_Hours_App.Models
{
    public class MyHoursDashboard
    {
        public int MyHoursDashboardId { get; set; }
        [Display(Name = "Select Module")]
        public int ModulesDashboardId { get; set; }
        public virtual ModulesDashboard ModulesDashboard { get; set; }
        [Display(Name = "Number of hours spent on module")]
        public int NumOfHoursSpent { get; set; }
        [Display(Name = "Self Hours left to spend on module")]
        public int NumOfHoursLeft { get; set; }
        [Display(Name = "Date and Time worked on module")]
        public DateTime Dateworked { get; set; }
        public string UserId { get; set; }
    }
}

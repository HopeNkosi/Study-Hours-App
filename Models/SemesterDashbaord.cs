using System;
using System.ComponentModel.DataAnnotations;

namespace Study_Hours_App.Models
{
    public class SemesterDashbaord
    {
        public int SemesterDashbaordId { get; set; }
        [Display(Name = "Period Name")]
        public string SemesterName { get; set; }
        [Display(Name = "Duration of Period")]
        public int SemesterDuration { get; set; }
        [Display(Name = "Period Start Date")]
        public DateTime SemesterStartDate { get; set; }
        [Display(Name = "Period End Date")]
        public DateTime SemesterEndDate { get; set; }
        public string UserId { get; set; }
    }
}


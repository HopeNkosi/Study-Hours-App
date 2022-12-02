using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Study_Hours_App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Study_Hours_App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ModulesDashboard> ModulesDashboard { get; set; }
        public DbSet<SemesterDashbaord> SemesterDashbaord { get; set; }
        public DbSet<MyHoursDashboard> MyHoursDashboard { get; set; }
        public DbSet<SelfStudyHours> SelfStudyHours { get; set; }
    }
}

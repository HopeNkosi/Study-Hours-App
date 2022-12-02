using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Study_Hours_App.Data;
using Study_Hours_App.Models;

namespace Study_Hours_App.Controllers
{
    public class MyHoursDashboardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyHoursDashboardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyHoursDashboards
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.MyHoursDashboard.Include(m => m.ModulesDashboard);
            return View(await applicationDbContext.Where(x => x.UserId == userId).ToListAsync());
        }

        // GET: MyHoursDashboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myHoursDashboard = await _context.MyHoursDashboard
                .Include(m => m.ModulesDashboard)
                .FirstOrDefaultAsync(m => m.MyHoursDashboardId == id);
            if (myHoursDashboard == null)
            {
                return NotFound();
            }

            return View(myHoursDashboard);
        }

        // GET: MyHoursDashboards/Create
        public IActionResult Create()
        {
            ViewData["ModulesDashboardId"] = new SelectList(_context.ModulesDashboard, "ModulesDashboardId", "ModuleName");
            return View();
        }

        // POST: MyHoursDashboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MyHoursDashboardId,ModulesDashboardId,NumOfHoursSpent,NumOfHoursLeft,Dateworked,UserId")] MyHoursDashboard myHoursDashboard)
        {
            var ModulesId = (from ds in _context.ModulesDashboard where ds.ModulesDashboardId == myHoursDashboard.ModulesDashboardId select ds.ModulesDashboardId).FirstOrDefault();
            var selfstudy = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.MySelfStudy).FirstOrDefault();
            var programcode = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.ModuleCode).FirstOrDefault();
            var modulename = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.ModuleName).FirstOrDefault();
            var credits = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.ModuleNumberOfCredits).FirstOrDefault();
            var weeks = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.SemesterDashbaordId).FirstOrDefault();
            var semesternumberofweeks = (from b in _context.SemesterDashbaord where b.SemesterDashbaordId == weeks select b.SemesterName).FirstOrDefault();
            if (ModelState.IsValid)
            {
                myHoursDashboard.NumOfHoursLeft = (int)(selfstudy - myHoursDashboard.NumOfHoursSpent);
                myHoursDashboard.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(myHoursDashboard);
                await _context.SaveChangesAsync();

                SelfStudyHours selfStudy = new SelfStudyHours();
                selfStudy.ModuleCode = programcode;
                selfStudy.ModuleName = modulename;
                selfStudy.SelfStudyHour = Convert.ToString(selfstudy);
                selfStudy.ModuleCredits = Convert.ToString(credits);
                selfStudy.Semester = semesternumberofweeks;
                selfStudy.NumOfHours = Convert.ToString(myHoursDashboard.NumOfHoursSpent);
                selfStudy.NumOfHoursLeft = Convert.ToString(myHoursDashboard.NumOfHoursLeft);
                selfStudy.UserId =myHoursDashboard.UserId;
                _context.Add(selfStudy);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ModulesDashboardId"] = new SelectList(_context.ModulesDashboard, "ModulesDashboardId", "ModuleName", myHoursDashboard.ModulesDashboardId);
            return View(myHoursDashboard);
        }

        public async Task<IActionResult> Graphs()
        {
            return View();
        }

        // GET: MyHoursDashboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myHoursDashboard = await _context.MyHoursDashboard.FindAsync(id);
            if (myHoursDashboard == null)
            {
                return NotFound();
            }
            ViewData["ModulesDashboardId"] = new SelectList(_context.ModulesDashboard, "ModulesDashboardId", "ModuleName", myHoursDashboard.ModulesDashboardId);
            return View(myHoursDashboard);
        }

        // POST: MyHoursDashboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MyHoursDashboardId,ModulesDashboardId,NumOfHoursSpent,NumOfHoursLeft,Dateworked,UserId")] MyHoursDashboard myHoursDashboard)
        {
            var ModulesId = (from ds in _context.ModulesDashboard where ds.ModulesDashboardId == myHoursDashboard.ModulesDashboardId select ds.ModulesDashboardId).FirstOrDefault();
            var selfstudy = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.MySelfStudy).FirstOrDefault();
            var programcode = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.ModuleCode).FirstOrDefault();
            var modulename = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.ModuleName).FirstOrDefault();
            var credits = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.ModuleNumberOfCredits).FirstOrDefault();
            var weeks = (from b in _context.ModulesDashboard where b.ModulesDashboardId == ModulesId select b.SemesterDashbaordId).FirstOrDefault();
            var semesternumberofweeks = (from b in _context.SemesterDashbaord where b.SemesterDashbaordId == weeks select b.SemesterName).FirstOrDefault();
            if (id != myHoursDashboard.MyHoursDashboardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    myHoursDashboard.NumOfHoursLeft = (int)(selfstudy - myHoursDashboard.NumOfHoursSpent);
                    myHoursDashboard.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    _context.Update(myHoursDashboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyHoursDashboardExists(myHoursDashboard.MyHoursDashboardId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModulesDashboardId"] = new SelectList(_context.ModulesDashboard, "ModulesDashboardId", "ModuleName", myHoursDashboard.ModulesDashboardId);
            return View(myHoursDashboard);
        }

        // GET: MyHoursDashboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myHoursDashboard = await _context.MyHoursDashboard
                .Include(m => m.ModulesDashboard)
                .FirstOrDefaultAsync(m => m.MyHoursDashboardId == id);
            if (myHoursDashboard == null)
            {
                return NotFound();
            }

            return View(myHoursDashboard);
        }

        // POST: MyHoursDashboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myHoursDashboard = await _context.MyHoursDashboard.FindAsync(id);
            _context.MyHoursDashboard.Remove(myHoursDashboard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyHoursDashboardExists(int id)
        {
            return _context.MyHoursDashboard.Any(e => e.MyHoursDashboardId == id);
        }
    }
}

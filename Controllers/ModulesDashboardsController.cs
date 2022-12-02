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
    public class ModulesDashboardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModulesDashboardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ModulesDashboards
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicationDbContext = _context.ModulesDashboard.Include(m => m.SemesterDashbaord);
            return View(await applicationDbContext.Where(x => x.UserId == userId).ToListAsync());
        }

        // GET: ModulesDashboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modulesDashboard = await _context.ModulesDashboard
                .Include(m => m.SemesterDashbaord)
                .FirstOrDefaultAsync(m => m.ModulesDashboardId == id);
            if (modulesDashboard == null)
            {
                return NotFound();
            }

            return View(modulesDashboard);
        }

        // GET: ModulesDashboards/Create
        public IActionResult Create()
        {
            ViewData["SemesterDashbaordId"] = new SelectList(_context.SemesterDashbaord, "SemesterDashbaordId", "SemesterName");
            return View();
        }

        // POST: ModulesDashboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModulesDashboardId,ModuleCode,ModuleName,ModuleNumberOfCredits,ClassHoursPerWeek,MySelfStudy,SemesterDashbaordId,UserId")] ModulesDashboard modulesDashboard)
        {
            var semesterId = (from ds in _context.SemesterDashbaord where ds.SemesterDashbaordId == modulesDashboard.SemesterDashbaordId select ds.SemesterDashbaordId).FirstOrDefault();
            var semesternumberofweeks = (from b in _context.SemesterDashbaord where b.SemesterDashbaordId == semesterId select b.SemesterDuration).FirstOrDefault();
            if (ModelState.IsValid)
            {
                modulesDashboard.MySelfStudy =(((modulesDashboard.ModuleNumberOfCredits * 10) / semesternumberofweeks) - modulesDashboard.ClassHoursPerWeek);
                modulesDashboard.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(modulesDashboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SemesterDashbaordId"] = new SelectList(_context.SemesterDashbaord, "SemesterDashbaordId", "SemesterName", modulesDashboard.SemesterDashbaordId);
            return View(modulesDashboard);
        }

        // GET: ModulesDashboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modulesDashboard = await _context.ModulesDashboard.FindAsync(id);
            if (modulesDashboard == null)
            {
                return NotFound();
            }
            ViewData["SemesterDashbaordId"] = new SelectList(_context.SemesterDashbaord, "SemesterDashbaordId", "SemesterName", modulesDashboard.SemesterDashbaordId);
            return View(modulesDashboard);
        }

        // POST: ModulesDashboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModulesDashboardId,ModuleCode,ModuleName,ModuleNumberOfCredits,ClassHoursPerWeek,MySelfStudy,SemesterDashbaordId,UserId")] ModulesDashboard modulesDashboard)
        {
            var semesterId = (from ds in _context.SemesterDashbaord where ds.SemesterDashbaordId == modulesDashboard.SemesterDashbaordId select ds.SemesterDashbaordId).FirstOrDefault();
            var semesternumberofweeks = (from b in _context.SemesterDashbaord where b.SemesterDashbaordId == semesterId select b.SemesterDuration).FirstOrDefault();
            if (id != modulesDashboard.ModulesDashboardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    modulesDashboard.MySelfStudy = ((modulesDashboard.ModuleNumberOfCredits * 10) / semesternumberofweeks) - modulesDashboard.ClassHoursPerWeek;
                    modulesDashboard.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    _context.Update(modulesDashboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModulesDashboardExists(modulesDashboard.ModulesDashboardId))
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
            ViewData["SemesterDashbaordId"] = new SelectList(_context.SemesterDashbaord, "SemesterDashbaordId", "SemesterName", modulesDashboard.SemesterDashbaordId);
            return View(modulesDashboard);
        }

        // GET: ModulesDashboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modulesDashboard = await _context.ModulesDashboard
                .Include(m => m.SemesterDashbaord)
                .FirstOrDefaultAsync(m => m.ModulesDashboardId == id);
            if (modulesDashboard == null)
            {
                return NotFound();
            }

            return View(modulesDashboard);
        }

        // POST: ModulesDashboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modulesDashboard = await _context.ModulesDashboard.FindAsync(id);
            _context.ModulesDashboard.Remove(modulesDashboard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModulesDashboardExists(int id)
        {
            return _context.ModulesDashboard.Any(e => e.ModulesDashboardId == id);
        }
    }
}

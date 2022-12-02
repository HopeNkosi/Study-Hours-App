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
    public class SelfStudyHoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelfStudyHoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SelfStudyHours
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _context.SelfStudyHours.Where(x => x.UserId == userId).ToListAsync());
        }

        // GET: SelfStudyHours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selfStudyHours = await _context.SelfStudyHours
                .FirstOrDefaultAsync(m => m.SelfStudyHoursId == id);
            if (selfStudyHours == null)
            {
                return NotFound();
            }

            return View(selfStudyHours);
        }

        // GET: SelfStudyHours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SelfStudyHours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SelfStudyHoursId,ModuleCode,ModuleName,ModuleCredits,SelfStudyHour,Semester,NumOfHours,NumOfHoursLeft,UserId")] SelfStudyHours selfStudyHours)
        {
            if (ModelState.IsValid)
            {
                selfStudyHours.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(selfStudyHours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(selfStudyHours);
        }

        // GET: SelfStudyHours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selfStudyHours = await _context.SelfStudyHours.FindAsync(id);
            if (selfStudyHours == null)
            {
                return NotFound();
            }
            return View(selfStudyHours);
        }

        // POST: SelfStudyHours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SelfStudyHoursId,ModuleCode,ModuleName,ModuleCredits,SelfStudyHour,Semester,NumOfHours,NumOfHoursLeft,UserId")] SelfStudyHours selfStudyHours)
        {
            if (id != selfStudyHours.SelfStudyHoursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    selfStudyHours.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    _context.Update(selfStudyHours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SelfStudyHoursExists(selfStudyHours.SelfStudyHoursId))
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
            return View(selfStudyHours);
        }

        // GET: SelfStudyHours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selfStudyHours = await _context.SelfStudyHours
                .FirstOrDefaultAsync(m => m.SelfStudyHoursId == id);
            if (selfStudyHours == null)
            {
                return NotFound();
            }

            return View(selfStudyHours);
        }

        // POST: SelfStudyHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var selfStudyHours = await _context.SelfStudyHours.FindAsync(id);
            _context.SelfStudyHours.Remove(selfStudyHours);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SelfStudyHoursExists(int id)
        {
            return _context.SelfStudyHours.Any(e => e.SelfStudyHoursId == id);
        }
    }
}

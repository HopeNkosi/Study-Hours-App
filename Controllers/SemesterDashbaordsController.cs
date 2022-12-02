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
    public class SemesterDashbaordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SemesterDashbaordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SemesterDashbaords
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _context.SemesterDashbaord.Where(x => x.UserId == userId).ToListAsync());
        }

        // GET: SemesterDashbaords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semesterDashbaord = await _context.SemesterDashbaord
                .FirstOrDefaultAsync(m => m.SemesterDashbaordId == id);
            if (semesterDashbaord == null)
            {
                return NotFound();
            }

            return View(semesterDashbaord);
        }

        // GET: SemesterDashbaords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SemesterDashbaords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemesterDashbaordId,SemesterName,SemesterDuration,SemesterStartDate,SemesterEndDate,UserId")] SemesterDashbaord semesterDashbaord)
        {

            if (ModelState.IsValid)
            {
                semesterDashbaord.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(semesterDashbaord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semesterDashbaord);
        }

        // GET: SemesterDashbaords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semesterDashbaord = await _context.SemesterDashbaord.FindAsync(id);
            if (semesterDashbaord == null)
            {
                return NotFound();
            }
            return View(semesterDashbaord);
        }

        // POST: SemesterDashbaords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SemesterDashbaordId,SemesterName,SemesterDuration,SemesterStartDate,SemesterEndDate,UserId")] SemesterDashbaord semesterDashbaord)
        {
            if (id != semesterDashbaord.SemesterDashbaordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    semesterDashbaord.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    _context.Update(semesterDashbaord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemesterDashbaordExists(semesterDashbaord.SemesterDashbaordId))
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
            return View(semesterDashbaord);
        }

        // GET: SemesterDashbaords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semesterDashbaord = await _context.SemesterDashbaord
                .FirstOrDefaultAsync(m => m.SemesterDashbaordId == id);
            if (semesterDashbaord == null)
            {
                return NotFound();
            }

            return View(semesterDashbaord);
        }

        // POST: SemesterDashbaords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semesterDashbaord = await _context.SemesterDashbaord.FindAsync(id);
            _context.SemesterDashbaord.Remove(semesterDashbaord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SemesterDashbaordExists(int id)
        {
            return _context.SemesterDashbaord.Any(e => e.SemesterDashbaordId == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.Models.Enums;

namespace DemoMVC.Controllers
{
    [Route("dot-hoc")]
    public class BatchController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: Batch
        [Route("")]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var batches = _context.Batches
                .Include(b => b.Course)
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearch = searchString.ToLower();
                batches = batches.Where(c =>
                    c.BatchName.ToLower().Contains(normalizedSearch) ||
                    c.BatchCode.ToLower().Contains(normalizedSearch) ||
                    c.Course!.CourseName.ToLower().Contains(normalizedSearch));
            }
            return View(await batches.AsNoTracking().ToListAsync());
        }

        // GET: Batch/Create
        [Route("tao-moi")]
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseName");
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(BatchStatus)));
            return View();
        }

        // POST: Batch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tao-moi")]
        public async Task<IActionResult> Create([Bind("Id,BatchCode,BatchName,StartDate,EndDate,Description,Status,CourseId")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(batch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseName", batch.CourseId);
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(BatchStatus)));
            return View(batch);
        }

        // GET: Batch/Edit/5
        [Route("chinh-sua/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseName", batch.CourseId);
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(BatchStatus)));
            return View(batch);
        }

        // POST: Batch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("chinh-sua/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BatchCode,BatchName,StartDate,EndDate,Description,Status,CourseId")] Batch batch)
        {
            if (id != batch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(batch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BatchExists(batch.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseName", batch.CourseId);
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(BatchStatus)));
            return View(batch);
        }

        // GET: Batch/Delete/5
        [Route("xoa/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batches
                .Include(b => b.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }

        // POST: Batch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("xoa/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch != null)
            {
                _context.Batches.Remove(batch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BatchExists(int id)
        {
            return _context.Batches.Any(e => e.Id == id);
        }
    }
}

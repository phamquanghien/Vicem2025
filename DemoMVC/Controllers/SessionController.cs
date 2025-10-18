using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.Models.Enums;

namespace DemoMVC.Controllers
{
    [Route("thoi-khoa-bieu")]
    public class SessionController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: Session
        [Route("")]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var sessions = _context.Sessions
                .Include(s => s.Batch)
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearch = searchString.ToLower();
                sessions = sessions.Where(c =>
                    c.Batch!.BatchName.ToLower().Contains(normalizedSearch));
            }
            return View(await sessions.AsNoTracking().ToListAsync());
        }

        // GET: Session/Create
        [Route("tao-moi")]
        public IActionResult Create()
        {
            ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "BatchCode");
            ViewBag.SessionType = new SelectList(Enum.GetValues(typeof(SessionType)));
            return View();
        }

        // POST: Session/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tao-moi")]
        public async Task<IActionResult> Create([Bind("Id,SessionDate,SessionType,StartTime,EndTime,BatchId")] Session session)
        {
            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "BatchCode", session.BatchId);
            ViewBag.SessionType = new SelectList(Enum.GetValues(typeof(SessionType)));
            return View(session);
        }

        // GET: Session/Edit/5
        [Route("chinh-sua/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "BatchCode", session.BatchId);
            ViewBag.SessionType = new SelectList(Enum.GetValues(typeof(SessionType)));
            return View(session);
        }

        // POST: Session/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("chinh-sua/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SessionDate,SessionType,StartTime,EndTime,BatchId")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
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
            ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "BatchCode", session.BatchId);
            ViewBag.SessionType = new SelectList(Enum.GetValues(typeof(SessionType)));
            return View(session);
        }

        // GET: Session/Delete/5
        [Route("xoa/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Batch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Session/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("xoa/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}

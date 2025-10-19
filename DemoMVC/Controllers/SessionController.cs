using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.Models.Enums;
using DemoMVC.Models.DTOs.Session;

namespace DemoMVC.Controllers
{
    [Route("dot-hoc/thoi-khoa-bieu")]
    public class SessionController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: Session
        [Route("{batchId}")]
        public async Task<IActionResult> Index(string searchString, int batchId)
        {
            ViewData["CurrentFilter"] = searchString;
            var sessions = _context.Sessions
                .Include(s => s.Batch)
                .Where(s => s.BatchId == batchId)
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearch = searchString.ToLower();
                sessions = sessions.Where(c =>
                    c.Batch!.BatchName.ToLower().Contains(normalizedSearch));
            }
            ViewData["CurrentId"] = batchId;
            var batch = await _context.Batches.FindAsync(batchId);
            if (batch != null)
            {
                ViewBag.CurrentBatchName = batch.BatchName;
            }
            return View(await sessions.AsNoTracking().ToListAsync());
        }

        // GET: Session/Create
        [Route("tao-moi/{id?}")]
        public IActionResult Create(int? id)
        {
            var batch = _context.Batches.Find(id);
            if (batch != null)
            {
                ViewBag.CurrentBatchName = batch.BatchName;
                ViewBag.CurrentBatchId = batch.Id;
            }
            ViewBag.SessionType = new SelectList(Enum.GetValues(typeof(SessionType)));
            return View();
        }

        // POST: Session/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tao-moi/{id?}")]
        public async Task<IActionResult> Create([Bind("SessionDate,SessionType,StartTime,EndTime, BatchId")] CreateSessionDto createSessionDto, int id)
        {
            if (ModelState.IsValid)
            {
                var session = new Session
                {
                    SessionDate = createSessionDto.SessionDate,
                    SessionType = createSessionDto.SessionType,
                    StartTime = createSessionDto.StartTime,
                    EndTime = createSessionDto.EndTime,
                    BatchId = createSessionDto.BatchId
                };
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Session", new { batchId = createSessionDto.BatchId });
            }
            ViewBag.SessionType = new SelectList(Enum.GetValues(typeof(SessionType)));
            return View(createSessionDto);
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
            var updateSessionDto = new UpdateSessionDto
            {
                Id = session.Id,
                SessionDate = session.SessionDate,
                SessionType = session.SessionType,
                StartTime = session.StartTime,
                EndTime = session.EndTime
            };
            ViewBag.BatchId = session.BatchId;
            ViewBag.SessionType = new SelectList(Enum.GetValues(typeof(SessionType)));
            return View(updateSessionDto);
        }

        // POST: Session/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("chinh-sua/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SessionDate,SessionType,StartTime,EndTime")] UpdateSessionDto updateSessionDto)
        {
            if (id != updateSessionDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var session = await _context.Sessions.FindAsync(id);
                session!.SessionDate = updateSessionDto.SessionDate;
                session.SessionType = updateSessionDto.SessionType;
                session.StartTime = updateSessionDto.StartTime;
                session.EndTime = updateSessionDto.EndTime;
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                    ViewBag.BatchId = session.BatchId;
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
                return RedirectToAction(nameof(Index), "Session", new { batchId = session.BatchId });
            }

            ViewBag.SessionType = new SelectList(Enum.GetValues(typeof(SessionType)));
            return View(updateSessionDto);
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
            ViewBag.BatchId = session.BatchId;

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
            return RedirectToAction(nameof(Index), "Session", new { batchId = session?.BatchId ?? 1 });
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
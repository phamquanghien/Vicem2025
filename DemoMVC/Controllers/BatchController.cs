using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.Models.Enums;
using DemoMVC.Models.DTOs.Batch;

namespace DemoMVC.Controllers
{
    [Route("khoa-hoc/dot-hoc")]
    public class BatchController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: Batch
        [Route("{courseId}")]
        public async Task<IActionResult> Index(string searchString, int courseId)
        {
            ViewData["CurrentFilter"] = searchString;
            var batches = _context.Batches
                .Include(b => b.Course)
                .Where(b => b.CourseId == courseId)
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearch = searchString.ToLower();
                batches = batches.Where(c =>
                    c.BatchName.ToLower().Contains(normalizedSearch) ||
                    c.BatchCode.ToLower().Contains(normalizedSearch) ||
                    c.Course!.CourseName.ToLower().Contains(normalizedSearch));
            }
            ViewData["CurrentId"] = courseId;
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                ViewBag.CurrentCourseName = course.CourseName;
            }

            return View(await batches.AsNoTracking().ToListAsync());
        }

        // GET: Batch/Create
        [Route("tao-moi/{id}")]
        public IActionResult Create(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                ViewBag.CurrentCourseName = course.CourseName;
                ViewBag.CurrentCourseId = course.Id;
            }
            return View();
        }

        // POST: Batch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tao-moi/{id}")]
        public async Task<IActionResult> Create([Bind("BatchCode,BatchName,StartDate,EndDate,CourseId")] CreateBatchDto createBatchDto, int id)
        {
            if (id != createBatchDto.CourseId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var batch = new Batch
                {
                    BatchCode = createBatchDto.BatchCode,
                    BatchName = createBatchDto.BatchName,
                    StartDate = createBatchDto.StartDate,
                    EndDate = createBatchDto.EndDate,
                    CourseId = createBatchDto.CourseId,
                    Status = BatchStatus.NotStarted
                };
                _context.Add(batch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Batch", new { courseId = createBatchDto.CourseId });
            }
            return View(createBatchDto);
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
            var updateBatchDto = new UpdateBatchDto
            {
                Id = batch.Id,
                BatchCode = batch.BatchCode,
                BatchName = batch.BatchName,
                StartDate = batch.StartDate,
                EndDate = batch.EndDate,
                Status = batch.Status
            };
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(BatchStatus)));
            return View(updateBatchDto);
        }

        // POST: Batch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("chinh-sua/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BatchCode,BatchName,StartDate,EndDate,Description,Status")] UpdateBatchDto updateBatchDto)
        {
            if (id != updateBatchDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var batch = await _context.Batches.FindAsync(id);
                batch!.BatchCode = updateBatchDto.BatchCode;
                batch.BatchName = updateBatchDto.BatchName;
                batch.StartDate = updateBatchDto.StartDate;
                batch.EndDate = updateBatchDto.EndDate;
                batch.Status = updateBatchDto.Status;

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
                return RedirectToAction(nameof(Index), "Batch", new { courseId = batch.CourseId });
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(BatchStatus)));
            return View(updateBatchDto);
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
            return RedirectToAction(nameof(Index), "Batch", new { courseId = batch?.CourseId ?? 1 });
        }

        private bool BatchExists(int id)
        {
            return _context.Batches.Any(e => e.Id == id);
        }
    }
}
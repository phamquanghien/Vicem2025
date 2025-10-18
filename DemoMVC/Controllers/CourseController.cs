using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoMVC.Models.Enums;

namespace DemoMVC.Controllers
{
    [Route("khoa-hoc")]
    public class CourseController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: Course
        [Route("")]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var courses = from m in _context.Courses
                          select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearch = searchString.ToLower();
                courses = courses.Where(c =>
                    c.CourseName.ToLower().Contains(normalizedSearch) ||
                    c.CourseCode.ToLower().Contains(normalizedSearch));
            }
            return View(await courses.AsNoTracking().ToListAsync());
        }

        // GET: Course/Create
        [Route("tao-moi")]
        public IActionResult Create()
        {
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(CourseStatus)));
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tao-moi")]
        public async Task<IActionResult> Create([Bind("Id,CourseCode,CourseName,CreatedDate,Status")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(CourseStatus)));
            return View(course);
        }

        // GET: Course/Edit/5
        [Route("sua/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(CourseStatus)));
            return View(course);
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("sua/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseCode,CourseName,CreatedDate,Status")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(CourseStatus)));
            return View(course);
        }

        // GET: Course/Delete/5
        [Route("xoa/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("xoa/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Route("chi-tiet/{id?}")]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["CourseId"] = id;
            return View();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoMVC.Models.Enums;
using DemoMVC.Models.ViewModels;
using ExcelDataReader;
using DemoMVC.Models.DTOs;

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
        [Route("hoc-vien/{id?}")]
        public async Task<IActionResult> Trainee(int id)
        {
            var registrations = await _context.Registrations
                .Include(r => r.Trainee)
                .Include(r => r.Course)
                .Where(r => r.CourseId == id)
                .Select(r => new RegistrationVM
                {
                    Id = r.Id,
                    TraineeCode = r.Trainee.TraineeCode,
                    FullName = r.Trainee.FullName,
                    DateOfBirth = r.Trainee.DateOfBirth,
                    Gender = r.Trainee.Gender,
                    Organization = r.Trainee.Organization,
                    RegistrationDate = r.RegistrationDate,
                    Status = r.Status
                })
                .ToListAsync();
            ViewBag.CourseName = (await _context.Courses.FindAsync(id))?.CourseName;
            ViewBag.CourseId = id;
            return View(registrations);
        }
        [Route("nhap-hoc-vien/{courseId?}")]
        public async Task<IActionResult> ImportTrainees(int courseId)
        {
            ViewBag.CourseName = (await _context.Courses.FindAsync(courseId))?.CourseName;
            ViewBag.CourseId = courseId;
            return View();
        }
        [HttpPost]
        [Route("nhap-hoc-vien/{courseId?}")]
        public async Task<IActionResult> ImportTrainees(IFormFile file, int courseId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Vui lòng chọn file Excel để tải lên.");

            var trainees = new List<TraineeImportDto>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = file.OpenReadStream())
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = reader.AsDataSet();
                var table = dataSet.Tables[0];

                for (int i = 1; i < table.Rows.Count; i++)
                {
                    var row = table.Rows[i];
                    var trainee = new TraineeImportDto
                    {
                        TraineeCode = row[0]?.ToString()?.Trim() ?? "",
                        FullName = row[1]?.ToString()?.Trim() ?? "",
                        DateOfBirth = DateTime.TryParse(row[2]?.ToString(), out var d) ? d : null,
                        Gender = row[3]?.ToString()?.Trim(),
                        Organization = row[4]?.ToString()?.Trim()
                    };

                    if (!string.IsNullOrEmpty(trainee.TraineeCode))
                        trainees.Add(trainee);
                }
            }

            foreach (var item in trainees)
            {
                // Kiểm tra học viên có tồn tại chưa
                var existingTrainee = await _context.Trainees
                    .FirstOrDefaultAsync(x => x.TraineeCode == item.TraineeCode);

                if (existingTrainee == null)
                {
                    existingTrainee = new Trainee
                    {
                        TraineeCode = item.TraineeCode,
                        FullName = item.FullName,
                        DateOfBirth = item.DateOfBirth,
                        Gender = item.Gender,
                        Organization = item.Organization
                    };
                    _context.Trainees.Add(existingTrainee);
                    await _context.SaveChangesAsync();
                }

                // Kiểm tra đăng ký khóa học
                bool alreadyRegistered = await _context.Registrations
                    .AnyAsync(r => r.TraineeId == existingTrainee.Id && r.CourseId == courseId);

                if (!alreadyRegistered)
                {
                    _context.Registrations.Add(new Registration
                    {
                        CourseId = courseId,
                        TraineeId = existingTrainee.Id,
                        RegistrationDate = DateTime.Now,
                        Status = RegistrationStatus.Registered
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Trainee", "Course", new { id = courseId });
        }
        [HttpPost]
        [Route("hoc-vien/huy-hoc-vien/{id}")]
        public async Task<IActionResult> CancelRegistration(int courseId, int id)
        {
            // Tìm bản ghi đăng ký của học viên
            var registration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null)
            {
                return NotFound();
            }
            if (registration.Status != RegistrationStatus.Registered)
            {
                return BadRequest("Không thể huỷ đăng ký học viên.");
            }
            // Cập nhật trạng thái
            registration.Status = RegistrationStatus.Cancelled;

            await _context.SaveChangesAsync();
            return RedirectToAction("Trainee", "Course", new { id = courseId });
        }
        [HttpPost]
        [Route("hoc-vien/dang-ky/{id}")]
        public async Task<IActionResult> UpdateRegistration(int courseId, int id)
        {
            // Tìm bản ghi đăng ký của học viên
            var registration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null)
            {
                return NotFound();
            }
            if (registration.Status != RegistrationStatus.Cancelled)
            {
                return BadRequest("Không thể đăng ký học viên");
            }

            // Cập nhật trạng thái
            registration.Status = RegistrationStatus.Registered;

            await _context.SaveChangesAsync();
            return RedirectToAction("Trainee", "Course", new { id = courseId });
        }
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}

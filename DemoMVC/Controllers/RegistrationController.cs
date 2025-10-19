using DemoMVC.Data;
using DemoMVC.Models.DTOs.Trainee;
using DemoMVC.Models.Entities;
using DemoMVC.Models.Enums;
using DemoMVC.Models.ViewModels;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    [Route("khoa-hoc/hoc-vien")]
    public class RegistrationController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        [Route("danh-sach/{courseId}")]
        public async Task<IActionResult> Index(int courseId)
        {
            var registrations = await _context.Registrations
                .Include(r => r.Trainee)
                .Include(r => r.Course)
                .Where(r => r.CourseId == courseId)
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
            ViewData["CurrentId"] = courseId;
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                ViewBag.CurrentCourseName = course.CourseName;
            }
            return View(registrations);
        }
        [Route("nhap-hoc-vien/{id?}")]
        public async Task<IActionResult> ImportTrainees(int id)
        {
            ViewData["CurrentId"] = id;
            ViewData["CurrentCourseName"] = (await _context.Courses.FindAsync(id))?.CourseName;
            return View();
        }
        [HttpPost]
        [Route("nhap-hoc-vien/{id?}")]
        public async Task<IActionResult> ImportTrainees(IFormFile file, int id)
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
                    .AnyAsync(r => r.TraineeId == existingTrainee.Id && r.CourseId == id);

                if (!alreadyRegistered)
                {
                    _context.Registrations.Add(new Registration
                    {
                        CourseId = id,
                        TraineeId = existingTrainee.Id,
                        RegistrationDate = DateTime.Now,
                        Status = RegistrationStatus.Registered
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Registration", new { courseId = id });
        }
        [HttpPost]
        [Route("huy-dang-ky/{id}")]
        public async Task<IActionResult> CancelRegistration(int id)
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
            return RedirectToAction("Index", "Registration", new { courseId = registration.CourseId });
        }
        [HttpPost]
        [Route("dang-ky/{id}")]
        public async Task<IActionResult> UpdateRegistration(int id)
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
            return RedirectToAction("Index", "Registration", new { courseId = registration.CourseId });
        }
    }
}
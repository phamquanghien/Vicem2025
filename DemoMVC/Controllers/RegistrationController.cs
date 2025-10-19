using DemoMVC.Data;
using DemoMVC.Models.ViewModels;
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
    }
}
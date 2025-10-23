using DemoMVC.Areas.Admin.Models.DTOs.Course;
using DemoMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController(ICourseService _courseService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var course = await _courseService.GetAllCoursesAsync();
            return View(course);
        }
    }
}
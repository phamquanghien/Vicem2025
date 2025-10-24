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
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseDto createCourseDto)
        {
            if (ModelState.IsValid)
            {
                bool isAdded = await _courseService.CreateCourseAsync(createCourseDto);
                if (isAdded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Không thể thêm khóa học.");
            }
            return View(createCourseDto);
        }
    }
}
using DemoMVC.Areas.Admin.Models.DTOs.Course;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Services.Implementations
{
    public class CourseService(ApplicationDbContext context) : ICourseService
    {
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await context.Courses.ToListAsync();
        }
    }
}
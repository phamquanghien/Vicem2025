using DemoMVC.Areas.Admin.Models.DTOs.Course;
using DemoMVC.Models.Entities;

namespace DemoMVC.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
    }
}
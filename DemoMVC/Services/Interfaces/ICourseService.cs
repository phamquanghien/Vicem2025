using DemoMVC.Areas.Admin.Models.DTOs.Course;
using DemoMVC.Models.Entities;

namespace DemoMVC.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task<bool> CreateCourseAsync(CreateCourseDto createCourseDto);
        Task<bool> EditCourseAsync(EditCourseDto editCourseDto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
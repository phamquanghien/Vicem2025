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
        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await context.Courses.FindAsync(id);
        }

        public async Task<bool> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            if (createCourseDto == null) return false;
            var newCourse = new Course
            {
                CourseCode = createCourseDto.CourseCode,
                CourseName = createCourseDto.CourseName
            };

            context.Courses.Add(newCourse);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EditCourseAsync(EditCourseDto editCourseDto)
        {
            if (editCourseDto == null) return false;

            var currentCourse = await context.Courses.FindAsync(editCourseDto.CourseId);
            if (currentCourse == null) return false;

            currentCourse.CourseCode = editCourseDto.CourseCode;
            currentCourse.CourseName = editCourseDto.CourseName;
            context.Courses.Update(currentCourse);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await context.Courses.FindAsync(id);
            if (course == null) return false;

            context.Courses.Remove(course);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
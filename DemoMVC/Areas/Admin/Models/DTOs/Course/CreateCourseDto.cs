using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Areas.Admin.Models.DTOs.Course
{
    public class CreateCourseDto
    {
        [Required]
        public string CourseCode { get; set; } = default!;
        [Required]
        public string CourseName { get; set; } = default!;
    }
}
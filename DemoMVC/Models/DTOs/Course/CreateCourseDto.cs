using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.DTOs.Course
{
    public class CreateCourseDto
    {
        [Required, StringLength(50)]
        public string CourseCode { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string CourseName { get; set; } = string.Empty;
    }
}
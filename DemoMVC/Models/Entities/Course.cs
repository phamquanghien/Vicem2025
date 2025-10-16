using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string CourseCode { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string CourseName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
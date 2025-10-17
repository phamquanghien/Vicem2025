using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoMVC.Models.Enums;

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
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public CourseStatus Status { get; set; } = CourseStatus.NotStarted;
        public ICollection<Batch>? Batches { get; set; }
        public ICollection<Registration>? Registrations { get; set; }
    }
}
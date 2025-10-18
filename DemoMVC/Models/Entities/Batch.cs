using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoMVC.Models.Enums;

namespace DemoMVC.Models.Entities
{
    public class Batch
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string BatchCode { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string BatchName { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
        public BatchStatus Status { get; set; } = BatchStatus.NotStarted;
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course? Course { get; set; } = default!;
        public ICollection<Session>? Sessions { get; set; }
    }
}
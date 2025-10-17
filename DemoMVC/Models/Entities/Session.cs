using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entities
{
    public class Session
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string SessionCode { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string SessionName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required, StringLength(50)]
        public string Period { get; set; } = "Morning";

        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [ForeignKey(nameof(Batch))]
        public int BatchId { get; set; }

        // 🔗 Navigation properties
        public Batch Batch { get; set; } = default!;

        public ICollection<Attendance>? Attendances { get; set; }
    }
}
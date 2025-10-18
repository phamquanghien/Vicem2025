using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoMVC.Models.Enums;

namespace DemoMVC.Models.Entities
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime SessionDate { get; set; }

        [Required]
        public SessionType SessionType { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
        [ForeignKey(nameof(Batch))]
        public int BatchId { get; set; }

        // 🔗 Navigation properties
        public Batch? Batch { get; set; } = default!;

        public ICollection<Attendance>? Attendances { get; set; }
    }
}
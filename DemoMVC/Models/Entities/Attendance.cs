using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoMVC.Models.Enums;

namespace DemoMVC.Models.Entities
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int TraineeId { get; set; }
        [Required]
        public AttendanceStatus Status { get; set; } = AttendanceStatus.NotMarked;

        [DataType(DataType.DateTime)]
        public DateTime RecordTime { get; set; } = DateTime.Now;
        // Navigation
        public Session? Session { get; set; } = default!;
        public Trainee? Trainee { get; set; } = default!;
    }
}
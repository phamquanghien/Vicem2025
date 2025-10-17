using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entities
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        public int SessionId { get; set; }
        public int TraineeId { get; set; }

        public bool IsPresent { get; set; }
        public string? Note { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RecordTime { get; set; } = DateTime.Now;

        // Navigation
        public Session Session { get; set; } = default!;
        public Trainee Trainee { get; set; } = default!;
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entities
{
    public class Evaluation
    {
        [Key]
        public int Id { get; set; }

        public int TraineeId { get; set; }
        public int CourseId { get; set; }
        public double? Score { get; set; }
        [StringLength(500)]
        public string? Remark { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EvaluationDate { get; set; }
        // Navigation
        public Trainee Trainee { get; set; } = default!;
        public Course Course { get; set; } = default!;
    }
}
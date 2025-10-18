using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.ViewModels
{
    public class EvaluationItemVM
    {
        public int TraineeId { get; set; }
        public string TraineeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        [Range(0, 100)]
        public double? Score { get; set; }

        public bool IsPassed { get; set; }

        [StringLength(500)]
        public string? Remark { get; set; }
    }
}
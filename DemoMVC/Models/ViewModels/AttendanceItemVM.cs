using System.ComponentModel.DataAnnotations;
using DemoMVC.Models.Enums;

namespace DemoMVC.Models.ViewModels
{
    public class AttendanceItemVM
    {
        public int TraineeId { get; set; }
        public string TraineeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        [Required]
        public AttendanceStatus Status { get; set; } = AttendanceStatus.NotMarked;
    }
}
using System.ComponentModel.DataAnnotations;
using DemoMVC.Models.Enums;

namespace DemoMVC.Models.ViewModels
{
    public class AttendanceVM
    {
        public int SessionId { get; set; }

        [Display(Name = "Ngày học")]
        [DataType(DataType.Date)]
        public DateTime SessionDate { get; set; }

        [Required]
        [Display(Name = "Buổi học")]
        public SessionType SessionType { get; set; }

        [Display(Name = "Tên khóa học")]
        public string CourseName { get; set; } = string.Empty;

        [Display(Name = "Tên đợt học")]
        public string? BatchName { get; set; }

        // Danh sách học viên để điểm danh
        public List<AttendanceItemVM> Trainees { get; set; } = new();
    }
}
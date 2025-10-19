using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.DTOs.Trainee
{
    public class TraineeImportDto
    {
        // Thông tin học viên
        [Display(Name = "Mã học viên")]
        public string TraineeCode { get; set; } = string.Empty;

        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }
        [Display(Name = "Đơn vị")]
        public string? Organization { get; set; }
    }
}
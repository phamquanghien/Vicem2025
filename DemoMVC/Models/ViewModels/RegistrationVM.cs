using System.ComponentModel.DataAnnotations;
using DemoMVC.Models.Enums;

namespace DemoMVC.Models.ViewModels
{
    public class RegistrationVM
    {
        public int Id { get; set; }
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
        [Display(Name = "Ngày đăng ký")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "Trạng thái")]
        public RegistrationStatus Status { get; set; }
    }
}
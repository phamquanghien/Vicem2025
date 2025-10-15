using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        // Họ và tên đầy đủ
        public string FullName { get; set; } = default!;
        // Giới tính (Nam / Nữ / Khác)
        [Required(ErrorMessage = "Giới tính là bắt buộc.")]
        public string Gender { get; set; } = default!;
        // Năm sinh
        [Required(ErrorMessage = "Năm sinh là bắt buộc.")]
        [Range(1900, 2025, ErrorMessage = "Năm sinh không hợp lệ.")]
        public int YearOfBirth { get; set; }
        // Địa chỉ
        public string? Address { get; set; } = default!;
        // Số điện thoại
        public string? PhoneNumber { get; set; } = default!;
        // Email liên hệ
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = default!;
    }
}
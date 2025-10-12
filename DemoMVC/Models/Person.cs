namespace DemoMVC.Models
{
    public class Person
    {
        public int Id { get; set; }
        // Họ và tên đầy đủ
        public string FullName { get; set; } = default!;
        // Giới tính (Nam / Nữ / Khác)
        public string Gender { get; set; } = default!;
        // Năm sinh
        public int YearOfBirth { get; set; }
        // Địa chỉ
        public string Address { get; set; } = default!;
        // Số điện thoại
        public string PhoneNumber { get; set; } = default!;
        // Email liên hệ
        public string Email { get; set; } = default!;
    }
}
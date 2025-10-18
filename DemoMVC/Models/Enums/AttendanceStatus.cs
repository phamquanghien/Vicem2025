namespace DemoMVC.Models.Enums
{
    public enum AttendanceStatus
    {
        NotMarked = 0,     // Chưa điểm danh
        Present = 1,       // Có mặt đúng giờ
        Late = 2,          // Đến muộn
        LeaveEarly = 3,    // Về sớm
        Absent = 4         // Nghỉ học
    }
}
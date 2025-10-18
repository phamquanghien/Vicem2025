using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DemoMVC.Models.Entities
{
    public class Trainee
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string TraineeCode { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }
        [StringLength(255)]
        public string? Organization { get; set; }
        [Display(Name = "Ngày đăng ký")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public ICollection<Registration>? Registrations { get; set; }
    }
}
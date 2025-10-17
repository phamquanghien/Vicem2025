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

        [StringLength(100)]
        public string? Position { get; set; }
        [StringLength(255)]
        public string? CompanyName { get; set; }

        [EmailAddress, StringLength(255)]
        public string? Email { get; set; }

        [Phone, StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;
        public ICollection<Registration>? Registrations { get; set; }
    }
}
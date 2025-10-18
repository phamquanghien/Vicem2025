using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoMVC.Models.Enums;

namespace DemoMVC.Models.Entities
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }

        public int TraineeId { get; set; }
        public int CourseId { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public RegistrationStatus Status { get; set; } = RegistrationStatus.Registered;

        // Navigation
        public Trainee Trainee { get; set; } = default!;
        public Course Course { get; set; } = default!;
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoMVC.Models.Enums;

namespace DemoMVC.Models.DTOs.Session
{
    public class CreateOrUpdateSessionDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime SessionDate { get; set; }

        [Required]
        public SessionType SessionType { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
    }
}
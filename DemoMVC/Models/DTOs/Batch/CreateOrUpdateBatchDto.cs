using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.DTOs.Batch
{
    public class CreateOrUpdateBatchDto
    {
        [Required, StringLength(50)]
        public string BatchCode { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string BatchName { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
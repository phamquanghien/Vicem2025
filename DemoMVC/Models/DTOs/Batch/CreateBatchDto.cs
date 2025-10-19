using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.DTOs.Batch
{
    public class CreateBatchDto : CreateOrUpdateBatchDto
    {
        public int CourseId { get; set; }
    }
}
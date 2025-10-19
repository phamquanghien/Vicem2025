using DemoMVC.Models.Enums;

namespace DemoMVC.Models.DTOs.Batch
{
    public class UpdateBatchDto : CreateOrUpdateBatchDto
    {
        public int Id { get; set; }
        public BatchStatus Status { get; set; }
    }
}
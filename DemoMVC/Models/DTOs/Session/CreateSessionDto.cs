namespace DemoMVC.Models.DTOs.Session
{
    public class CreateSessionDto : CreateOrUpdateSessionDto
    {
        public int BatchId { get; set; }
    }
}
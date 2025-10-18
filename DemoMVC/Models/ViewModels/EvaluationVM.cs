namespace DemoMVC.Models.ViewModels
{
    public class EvaluationVM
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;

        public List<EvaluationItemVM> Trainees { get; set; } = new();
    }
}
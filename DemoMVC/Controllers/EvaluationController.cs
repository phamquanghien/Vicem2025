using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    public class EvaluationController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IActionResult> Index(int courseId)
        {
            var course = await _context.Courses
                .Include(c => c.Registrations)
                    .ThenInclude(r => r.Trainee)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null) return NotFound();

            var existingEvaluations = await _context.Evaluations
                .Where(e => e.CourseId == courseId)
                .ToListAsync();

            var model = new EvaluationVM
            {
                CourseId = course.Id,
                CourseCode = course.CourseCode,
                CourseName = course.CourseName,
                Trainees = course.Registrations.Select(r =>
                {
                    var eval = existingEvaluations.FirstOrDefault(e => e.TraineeId == r.TraineeId);
                    return new EvaluationItemVM
                    {
                        TraineeId = r.TraineeId,
                        TraineeCode = r.Trainee.TraineeCode,
                        FullName = r.Trainee.FullName,
                        Score = eval?.Score,
                        Remark = eval?.Remark
                    };
                }).ToList()
            };

            return View(model);
        }
        public async Task<IActionResult> Save(EvaluationVM model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            foreach (var trainee in model.Trainees)
            {
                var eval = await _context.Evaluations
                    .FirstOrDefaultAsync(e => e.CourseId == model.CourseId && e.TraineeId == trainee.TraineeId);

                if (eval == null)
                {
                    eval = new Evaluation
                    {
                        CourseId = model.CourseId,
                        TraineeId = trainee.TraineeId
                    };
                    _context.Evaluations.Add(eval);
                }

                eval.Score = trainee.Score ?? 0;
                eval.Remark = trainee.Remark;
                eval.EvaluationDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã lưu kết quả đánh giá thành công.";
            return RedirectToAction("Index", new { courseId = model.CourseId });
        }
    }
}
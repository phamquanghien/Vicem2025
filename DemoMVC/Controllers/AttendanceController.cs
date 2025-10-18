using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: attendance/mark/{sessionId}
        [HttpGet]
        // [Route("attendance/mark/{sessionId}")]
        public async Task<IActionResult> Mark(int sessionId)
        {
            var session = await _context.Sessions
                .Include(s => s.Batch)
                .ThenInclude(b => b!.Course)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                return NotFound();

            var trainees = await _context.Registrations
                .Include(r => r.Trainee)
                .Where(r => r.CourseId == session.Batch!.CourseId)
                .Select(r => new AttendanceItemVM
                {
                    TraineeId = r.Trainee.Id,
                    TraineeCode = r.Trainee.TraineeCode,
                    FullName = r.Trainee.FullName,
                    Status = _context.Attendances
                        .Where(a => a.SessionId == session.Id && a.TraineeId == r.Trainee.Id)
                        .Select(a => a.Status)
                        .FirstOrDefault()
                })
                .ToListAsync();

            var model = new AttendanceVM
            {
                SessionId = session.Id,
                SessionDate = session.SessionDate,
                SessionType = session.SessionType,
                CourseName = session.Batch!.Course!.CourseName,
                BatchName = session.Batch.BatchName,
                Trainees = trainees
            };

            return View(model);
        }

        // POST: attendance/mark
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Route("attendance/mark")]
        public async Task<IActionResult> Mark(AttendanceVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            foreach (var trainee in model.Trainees)
            {
                var attendance = await _context.Attendances
                    .FirstOrDefaultAsync(a => a.SessionId == model.SessionId && a.TraineeId == trainee.TraineeId);

                if (attendance == null)
                {
                    attendance = new Attendance
                    {
                        SessionId = model.SessionId,
                        TraineeId = trainee.TraineeId,
                        Status = trainee.Status,
                        RecordTime = DateTime.Now
                    };
                    _context.Attendances.Add(attendance);
                }
                else
                {
                    attendance.Status = trainee.Status;
                    attendance.RecordTime = DateTime.Now;
                    _context.Attendances.Update(attendance);
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Lưu điểm danh thành công!";
            return RedirectToAction("Mark", new { sessionId = model.SessionId });
        }
    }
}

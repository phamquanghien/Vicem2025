using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DemoMVC.Controllers
{
    public class ResultDemoController : Controller
    {
        public IActionResult ShowView()
        {
            ViewBag.Message = "Đây là ví dụ về ViewResult";
            return View();
        }

        public IActionResult RedirectToGoogle()
        {
            return Redirect("https://www.google.com");
        }

        public IActionResult GoToHomeIndex()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetJson()
        {
            var data = new
            {
                Id = 1,
                Name = "Pham Quang Hien",
                Message = "Đây là dữ liệu JSON từ Controller"
            };

            return Json(data);
        }

        public IActionResult DownloadFile()
        {
            var fileContent = Encoding.UTF8.GetBytes("Xin chào! Đây là nội dung file.");
            var fileName = "example.txt";
            return File(fileContent, "text/plain", fileName);
        }

        public IActionResult NotFoundExample()
        {
            return StatusCode(404, "Không tìm thấy tài nguyên yêu cầu!");
        }
    }
}
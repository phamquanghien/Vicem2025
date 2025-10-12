using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class PersonController : Controller
    {
        private static List<Person> _people = new List<Person>
        {
            new() { Id = 1, FullName = "Nguyễn Văn A", Gender = "Nam", YearOfBirth = 1995, Address = "Hà Nội", Email = "a@gmail.com", PhoneNumber = "0123456789"},
            new() { Id = 2, FullName = "Trần Thị B", Gender = "Nữ", YearOfBirth = 1990, Address = "TP.HCM", Email = "b@gmail.com", PhoneNumber = "0987654321"}
        };
        public IActionResult Index()
        {
            return View(_people);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = _people.Max(p => p.Id) + 1;
                _people.Add(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }
    }
}
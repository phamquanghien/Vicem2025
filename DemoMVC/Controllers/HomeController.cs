using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers;

[Route("trang-chu")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("chinh-sach-bao-mat")]
    public IActionResult Privacy()
    {
        return View();
    }

    [Route("hello/{name:alpha}")]
    public IActionResult Hello(string name)
    {
        return Content($"<h1>Hello {name}</h1>", "text/html");
    }
    [Route("tinh-tong/{a:int}/{b:min(10)}")]
    public IActionResult GetSum(int a, int b)
    {
        int tong = a + b;
        return Content($"<h1>Tong cua {a} va {b} la: {tong}</h1>", "text/html");
    }

    [Route("loi")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using FMCGWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FMCGWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[HttpGet]
        public IActionResult Index()
        {
            var userName = User.Identity.IsAuthenticated ? User.Identity.Name : "Guest";
            ViewBag.UserName = userName;

            var compName = Request.Cookies["SelectedCompanyName"];
            ViewBag.CompanyName = compName;

            var compCode = Convert.ToInt16(Request.Cookies["SelectedCompany"]);
            ViewBag.CompanyCode = compCode;
            //return View("~/Views/Home/Index.cshtml", "home");

            return View();
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

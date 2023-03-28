using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UX_UI_WEB_APP.Models;

namespace UX_UI_WEB_APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult LoginPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PyramidChain()
        {
            return View();
        }

        public IActionResult AboutPage()
        {
            return View();
        }

        public IActionResult ProductPage()
        {
            return View();
        }

        public IActionResult CartPage()
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
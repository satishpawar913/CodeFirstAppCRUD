using CodeFirstApproachCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeFirstApproachCRUD.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error(string message)
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = message
            };
            return View(errorViewModel);
        }

        public IActionResult ErrorDetails(string message, string stackTrace)
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = message,
                StackTrace = stackTrace
            };
            return View(errorViewModel);
        }
    }
}

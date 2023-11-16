using Microsoft.AspNetCore.Mvc;
using MSIT153Site.Models;
using System.Diagnostics;

namespace MSIT153Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext _context;
        public HomeController(ILogger<HomeController> logger, DemoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult First()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Members()
        {
            return View(_context.Members);
        }

        public IActionResult Address()
        {
            return View();
        }

        public IActionResult Promise()
        {
            return View();
        }

        public IActionResult Fetch()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
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
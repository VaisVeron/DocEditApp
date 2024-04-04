using Microsoft.AspNetCore.Mvc;

namespace DocEditApp.Controllers
{
    public class SummernoteController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public SummernoteController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

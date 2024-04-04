using Microsoft.AspNetCore.Mvc;

namespace DocEditApp.Controllers
{
    public class ApryseController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ApryseController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

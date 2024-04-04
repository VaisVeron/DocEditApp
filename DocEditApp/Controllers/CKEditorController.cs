using Microsoft.AspNetCore.Mvc;

namespace DocEditApp.Controllers
{
    public class CKEditorController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public CKEditorController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Orion_Books.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

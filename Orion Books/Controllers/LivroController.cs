using Microsoft.AspNetCore.Mvc;
using Orion_Books.Data;

namespace Orion_Books.Controllers
{
    public class LivroController : Controller
    {
        private readonly ApplicationDbContext Context;
        public LivroController(ApplicationDbContext context) {
            Context = context;
        }


        public IActionResult Index()
        {
            var Livros = Context.Livros.ToList();
            return View(Livros);
        }
    }
}

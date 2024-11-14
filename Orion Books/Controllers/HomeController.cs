using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Orion_Books.Interfaces;
using Orion_Books.Models;
using Orion_Books.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace Orion_Books.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILivroDAO _livroDao;
        private readonly IEmprestimoDAO _emprestimoDao;
        private readonly UserManager<Usuario> _userManager;

        public HomeController(ILogger<HomeController> logger, ILivroDAO livroDAO, IEmprestimoDAO emprestimoDao, UserManager<Usuario> userManager)
        {
            _logger = logger;
            _livroDao = livroDAO;
            _emprestimoDao = emprestimoDao;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<Livro> recommendations = await _emprestimoDao.BuildBookRecommendation(userID);
            return View(recommendations);
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

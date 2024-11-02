using Microsoft.AspNetCore.Mvc;
using Orion_Books.Data;
using Orion_Books.Interfaces;
using Orion_Books.Models;

namespace Orion_Books.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroDAO _livroDao;
        public LivroController(ILivroDAO livroDAO) {
            _livroDao = livroDAO;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Livro> Livros = await _livroDao.GetAll();
            return View(Livros);
        }

        public async Task<IActionResult> Detail(int id) 
        {
            Livro livro = await _livroDao.GetById(id);
            IEnumerable<Livro> livrosRecomendados = await _livroDao.GetLivroByGenero(livro.Genero);
            
            // Aqui é pra não trazer o mesmo Objeto do Livro atual que eu peguei o ID pra ser exibido na tela de Detalhes do Livro em Questão
            livrosRecomendados = livrosRecomendados.Where(l => l.Id != id & l.CapaURL != livro.CapaURL).ToList();


            var viewModel = new LivroDetailViewModel
            {
                Livro = livro,
                LivrosRecomendados = livrosRecomendados
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}

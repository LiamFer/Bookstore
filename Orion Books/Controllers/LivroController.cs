using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Orion_Books.DAO;
using Orion_Books.Data;
using Orion_Books.Interfaces;
using Orion_Books.Models;
using Orion_Books.ViewModels;
using System.Security.Claims;

namespace Orion_Books.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroDAO _livroDao;
        private readonly IEmprestimoDAO _emprestimoDao;
        private readonly UserManager<Usuario> _userManager;

        public LivroController(ILivroDAO livroDAO, IEmprestimoDAO emprestimoDao, UserManager<Usuario> userManager)
        {
            _livroDao = livroDAO;
            _emprestimoDao = emprestimoDao;
            _userManager = userManager;
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

        [HttpPost]
        public async Task<IActionResult> Create(Livro livro)
        {
            
            if (!ModelState.IsValid)
            {
                return View(livro);
            }
            _livroDao.Add(livro);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var livro = await _livroDao.GetById(id);
            return View(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Livro livro)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Erro ao tentar alterar Informações do Livro");
                return View("Edit", livro);
            }

            _livroDao.Update(livro);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrow(int id)
        {
            var livro = await _livroDao.GetById(id);
            return View(new LivroBorrowViewModel(){ Livro = livro});
        }

        [HttpPost]
        public async Task<IActionResult> SucessBorrow(LivroBorrowViewModel borrowVM) {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Mandando o Empréstimo pro DB se conseguir achar o Usuário
            if (userID != null)
            {
                var usuario = _userManager.FindByIdAsync(userID).Result;

                if (usuario != null)
                {
                    // Chamando a camada DAO pra acessar e inserir no meu Banco
                    borrowVM.emprestimo.Usuario = usuario;
                    _emprestimoDao.Add(borrowVM.emprestimo);

                    // Alterando o Status do Livro pra como não disponível
                    var livro = await _livroDao.GetById(borrowVM.emprestimo.LivroId);
                    livro.Disponivel = false;
                    _livroDao.Update(livro);
                }
            }

            return View();
        }

        public async Task<IActionResult> Library(int id)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID != null) {
                LibraryViewModel Livros = new LibraryViewModel() {
                    library = await _emprestimoDao.GetCurrentBooks(userID),
                    history = await _emprestimoDao.GetHistory(userID)
                };
                //IEnumerable<Emprestimo> Livros = ;
                return View(Livros);
            }
            return View();
        }


        public async Task<IActionResult> ReservedBook(int id) 
        {
            LivroBorrowViewModel livroData = await _livroDao.getBookWithLoan(id);
            return View(livroData); 
        }


        [HttpPost]
        public async Task<IActionResult> ReturnBook(int id)
        {
            LivroBorrowViewModel VM = await _livroDao.getBookWithLoan(id);
            VM.emprestimo.DataEntrega = DateTime.Now;
            VM.Livro.Disponivel = true;
            _livroDao.Update(VM.Livro);
            _emprestimoDao.Update(VM.emprestimo);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            Livro book = await _livroDao.GetById(id);
            return View(book);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            Livro book = await _livroDao.GetById(id);
            _livroDao.Delete(book);
            return RedirectToAction("Index");
        }
    }
}

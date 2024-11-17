using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Orion_Books.Data;
using Orion_Books.Interfaces;
using Orion_Books.Models;
using Orion_Books.ViewModels;
using System.Security.Claims;

namespace Orion_Books.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IUsuarioDAO _usuarioDAO;

        public ContaController(UserManager<Usuario> userManager,SignInManager<Usuario> signInManager,ApplicationDbContext context,IUsuarioDAO usuarioDAO)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _usuarioDAO = usuarioDAO;
        }

        public IActionResult Login()
        {
            var cache = new LoginViewModel();
            return View(cache);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) { 
                return View(loginVM);
            }
            var usuario = await _userManager.FindByEmailAsync(loginVM.Email);

            // Se ele encontrar o Usuário pelo email no Banco ai eu checo a senha
            if (usuario != null) {
                var passChecker = await _userManager.CheckPasswordAsync(usuario,loginVM.Password);
                if (passChecker)
                {
                    var result = await _signInManager.PasswordSignInAsync(usuario,loginVM.Password,false,false);
                    if (result.Succeeded) { 
                        // Se der certo ai ele é enviado pra aquela pagina do começo
                        return RedirectToAction("Index", "Home");
                    }

                }
                TempData["Erro"] = "Informações Incorretas!";
                return View(loginVM);
            }
            TempData["Erro"] = "Informações Incorretas!";
            return View(loginVM);
        }


        public IActionResult Register()
        {
            var cache = new RegisterViewModel();
            return View(cache);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var usuario = await _userManager.FindByEmailAsync(registerVM.Email);
            if (usuario != null) {
                TempData["Erro"] = "Esse Email já está sendo utilizado";
                return View(registerVM);
            }

            var novoUser = new Usuario()
            {
                Email = registerVM.Email,
                UserName = registerVM.Email,
                Endereco = registerVM.Endereco,
            };

            var novoUserAnswer = await _userManager.CreateAsync(novoUser, registerVM.Password);
            if (novoUserAnswer.Succeeded) { 
                await _userManager.AddToRoleAsync(novoUser,UserRoles.standardUser);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _usuarioDAO.GetById(UserID);
            if (user == null) return View("Error");
            var editUser = new EditUserViewModel()
            {
                id = user.Id,
                endereco = new Endereco()
                {
                    Id = user.EnderecoId,
                    Rua = user.Endereco.Rua,
                    Bairro = user.Endereco.Bairro,
                    Cidade = user.Endereco.Cidade,
                    Numero = user.Endereco.Numero

                }
            };
            return View(editUser);

        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserViewModel VM) {
            // depois consigo implementar alteração de email e senha
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Erro ao Editar Perfil");
                return View("EditUserProfile", VM);
            }
            var user = await _usuarioDAO.GetNoTracking(VM.id);

            // Atualizando as info do endereço
            user.Endereco.Rua = VM.endereco.Rua;
            user.Endereco.Bairro = VM.endereco.Bairro;
            user.Endereco.Cidade = VM.endereco.Cidade;
            user.Endereco.Numero = VM.endereco.Numero;

            _usuarioDAO.Update(user);
            return RedirectToAction("Index", "Home");
        }
    }
}

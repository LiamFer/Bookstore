using System.ComponentModel.DataAnnotations;
using Orion_Books.Models;

namespace Orion_Books.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Endereço de Email")]
        [Required(ErrorMessage = "Email é Obrigatório!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirme a Senha")]
        [Required(ErrorMessage = "Confirmar a Senha!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não coincidem!")]
        public string CheckPassword { get; set; }
        public Endereco Endereco { get; set; }
    }
}

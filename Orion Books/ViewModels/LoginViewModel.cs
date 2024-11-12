using System.ComponentModel.DataAnnotations;

namespace Orion_Books.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Endereço de Email")]
        [Required(ErrorMessage = "Endereço de Email é Obrigatório")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

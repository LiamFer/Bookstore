using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orion_Books.Models
{
    public class Usuario : IdentityUser
    {

        [ForeignKey("Endereco")]
        public int EnderecoId { get; set; }
       public Endereco Endereco { get; set; }
    }
}

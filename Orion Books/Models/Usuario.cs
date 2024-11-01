using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orion_Books.Models
{
    public class Usuario
    {
        [Key] public int Id { get; set; }
        public string Nome { get; set; }
        [ForeignKey("Endereco")]
        public int EnderecoId { get; set; }
       public Endereco Endereco { get; set; }
    }
}

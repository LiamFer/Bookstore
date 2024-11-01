using System.ComponentModel.DataAnnotations;

namespace Orion_Books.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        public string Rua {  get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public int Numero { get; set; }
    }
}
